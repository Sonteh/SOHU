using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damageTest = 10;

    [SyncVar]
    private int currentHealth;

    public delegate void HealthUpdateDelegate(int currentHealth, int maxHealth);

    public event HealthUpdateDelegate EventHealthUpdate;

    #region Server
    
    [Server]
    private void SetHealth(int value)
    {
        currentHealth = value;
        EventHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public override void OnStartServer() => SetHealth(maxHealth);

    [Command]
    private void CmdDealDamage() => SetHealth(Mathf.Max(currentHealth - damageTest, 0));


    #endregion

    #region Client

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) {return;}

        if(!Input.GetKeyDown(KeyCode.Space)) {return;}

        CmdDealDamage();
    }

    #endregion
}
