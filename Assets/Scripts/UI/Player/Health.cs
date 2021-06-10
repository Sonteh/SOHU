using UnityEngine;
using Mirror;
using System;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] NetworkPlayer networkPlayer;
    //[SerializeField] PlayerUI playerUI;
    private float damage;
    GameObject roundSystem;
    //[SyncVar(hook = nameof(OnCurrentHealthChanged))] 
    [SyncVar]
    public float currentHealth;
    public delegate void HealthUpdateDelegate(float currentHealth, float maxHealth);
    public event HealthUpdateDelegate EventHealthUpdate;
    
    private UIScript uiScript; 

    private void Awake()
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }
    
    private void Start()
    {
        roundSystem = GameObject.Find("RoundSystem(Clone)");
    }
    
    [Server]
    private void SetHealth(float value)
    {
        currentHealth = value;
        //CmdSendPlayerHealth(currentHealth);
        EventHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public override void OnStartAuthority()
    {
        uiScript.health = this;
    }

    // private void OnCurrentHealthChanged(float _old, float _new)
    // {
    //     uiScript.playerHealth = currentHealth;
    // }

    // [Command]
    // public void CmdSendPlayerHealth(float _health)
    // {   
    //     uiScript.playerHealth = _health;
    // }

    public override void OnStartServer()
    {
        SetHealth(maxHealth);
    }

    [Command]
    private void CmdDealDamage(float value)
    {
        SetHealth(Mathf.Max(currentHealth - value, 0));

        if (currentHealth == 0f)
        {
            RpcOnDeath();
            roundSystem.GetComponent<RoundSystem>().OnDeath(connectionToClient);
        }
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

    private void Update() 
    {
        if (!hasAuthority) {return;}

        uiScript.PlayerHealth(currentHealth);
    }
}