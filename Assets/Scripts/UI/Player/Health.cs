using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] NetworkPlayer networkPlayer;
    [SerializeField] PlayerUI playerUI;
    private float damage;
    [SyncVar]
    private float currentHealth;
    public delegate void HealthUpdateDelegate(float currentHealth, float maxHealth);
    public event HealthUpdateDelegate EventHealthUpdate;
    GameObject roundSystem;
    
    private void Start()
    {
        roundSystem = GameObject.Find("RoundSystem(Clone)");
    }
    
    [Server]
    private void SetHealth(float value)
    {
        currentHealth = value;
        EventHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public override void OnStartServer() => SetHealth(maxHealth);

    [Command]
    private void CmdDealDamage(float value)
    {
        SetHealth(Mathf.Max(currentHealth - value, 0));
        //playerUI.PlayerHealthBar(currentHealth);

        if (currentHealth == 0f)
        {
            RpcOnDeath();
            roundSystem.GetComponent<RoundSystem>().OnDeath(connectionToClient);
        }
    }

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) {return;}
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if(!hasAuthority) {return;}

        if (collider.tag == "Spell")
        {   
            damage = collider.gameObject.GetComponent<SpellData>().spellDamage;
            CmdDealDamage(damage);
        }
    }

    private void OnTriggerStay(Collider collider) 
    {
        if (collider.tag == "Zone")
        {   
            damage = collider.gameObject.GetComponent<ZoneData>().zoneDamage;
            CmdDealDamage(damage);
        }
    }
}
