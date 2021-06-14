using UnityEngine;
using Mirror;
using System;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] NetworkPlayer networkPlayer;
    private float damage;
    private float heal;
    GameObject roundSystem; 
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
        EventHealthUpdate?.Invoke(currentHealth, maxHealth);
        TargetSetHealthInPlayerUi(currentHealth);

        if (currentHealth == 0f)
        {
            RpcOnDeath();
            roundSystem.GetComponent<RoundSystem>().OnDeath(connectionToClient);
        }
    }

    public override void OnStartServer()
    {
        SetHealth(maxHealth);
    }

    [TargetRpc]
    private void TargetSetHealthInPlayerUi(float currentHealth)
    {
        uiScript.PlayerHealth(currentHealth);
    }

    [Command]
    private void CmdDealDamage(float value)
    {
        SetHealth(Mathf.Max(currentHealth - value, 0));
    }

    [Command]
    public void CmdHealDamage(float value)
    {
        if (currentHealth <= 100f)
        {
            SetHealth(Mathf.Min(currentHealth + value, 100f));
        }
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (!hasAuthority) { return; }

        if (collider.tag == "Spell")
        {   
            damage = collider.gameObject.GetComponent<SpellData>().spellDamage;
            CmdDealDamage(damage);
        }
        if (collider.tag == "Heal")
        {   
            heal = collider.gameObject.GetComponent<SpellData>().healAmount;
            CmdHealDamage(heal);
        }
    }

    private void OnTriggerStay(Collider collider) 
    {
        if (!hasAuthority) { return; }
        
        if (collider.tag == "Zone")
        {   
            damage = collider.gameObject.GetComponent<ZoneData>().zoneDamage;
            CmdDealDamage(damage);
        }
        if (collider.tag == "HealOverTime")
        {   
            heal = collider.gameObject.GetComponent<SpellData>().healAmount;
            CmdHealDamage(heal);
        }
    }
}