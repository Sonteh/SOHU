using UnityEngine;
using Mirror;
using System;

public class EnvironmentDurability : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] public float maxHealth = 100f;
    //[SerializeField] NetworkPlayer networkPlayer;
    private float damage;
    private float heal;
    [SerializeField] GameObject roundSystem;
    [SerializeField] GameObject destroyedModel;
    //GameObject roundSystem; 
    [SyncVar]
    public float currentHealth;
    public delegate void HealthUpdateDelegate(float currentHealth, float maxHealth);
    public event HealthUpdateDelegate EventHealthUpdate;

    [Server]
    private void SetHealth(float value)
    {
        currentHealth = value;
        EventHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public override void OnStartServer()
    {
        SetHealth(maxHealth);
    }

    //[Command]
    private void CmdDealDamage(float value)
    {
        Debug.Log("Witam w deal damage");
        SetHealth(Mathf.Max(currentHealth - value, 0));
        Debug.Log("co z tym max hp?: "+currentHealth);
        if (currentHealth == 0f)
        {
            RpcOnDeath();
            //roundSystem.GetComponent<RoundSystem>().OnDeath(connectionToClient);
        }
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
        destroyedModel.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("sdddddddddddddddddddddddddddd");
        
        
        if (collider.tag == "Spell")
        {   
            damage = collider.gameObject.GetComponent<SpellData>().spellDamage;
            Debug.Log("DEMEJDZ: "+ damage);
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