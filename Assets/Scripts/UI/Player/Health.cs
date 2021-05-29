using UnityEngine;
using Mirror;
using System;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] NetworkPlayer networkPlayer;
    private float damage;
    [SyncVar] private float currentHealth;
    public delegate void HealthUpdateDelegate(float currentHealth, float maxHealth);
    public event HealthUpdateDelegate EventHealthUpdate;
    GameObject roundSystem;

    //[SyncVar] public int numPlayers;
    //[SyncVar(hook = nameof(HandlePlayerNumberChanged))]
    //public int numPlayers;

    //public void HandlePlayerNumberChanged(int oldValue, int newValue) => UpdatePlayers();
    
    private void Start()
    {
        roundSystem = GameObject.Find("RoundSystem(Clone)");
    }
    
    #region Server
    
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

        if (currentHealth == 0f)
        {
            RpcOnDeath();
            roundSystem.GetComponent<RoundSystem>().OnDeath(connectionToClient);
        }
    }

    #endregion

    #region Client

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) {return;}
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        //networkPlayer.PlayerDeath(true);
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

    #endregion
}
