using UnityEngine;
using Mirror;
using System;

public class EnvironmentDurability : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] public float maxHealth = 100f;
    private float damage;
    [SerializeField] GameObject destroyedModel;
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

    private void CmdDealDamage(float value)
    {
        SetHealth(Mathf.Max(currentHealth - value, 0));
        if (currentHealth == 0f)
        {
            RpcOnDeath();
        }
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        destroyedModel.SetActive(true);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Spell")
        {   
            damage = collider.gameObject.GetComponent<SpellData>().spellDamage;
            CmdDealDamage(damage);
            Destroy(collider.gameObject);
        }
    }
}