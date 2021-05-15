using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float damage;
    [SyncVar] private float currentHealth;
    public delegate void HealthUpdateDelegate(float currentHealth, float maxHealth);
    public event HealthUpdateDelegate EventHealthUpdate;

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
    }

    #endregion

    #region Client

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) {return;}

        //if(!Input.GetKeyDown(KeyCode.Space)) {return;}
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
    #endregion
}
