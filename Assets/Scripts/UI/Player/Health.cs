using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float damage;

    [SyncVar]
    private float currentHealth;

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

        if(!Input.GetKeyDown(KeyCode.Space)) {return;}

        //CmdDealDamage();
    }
    
    [ClientCallback]
    private void OnTriggerEnter(Collider other)
    {
        if(!hasAuthority) {return;}

        if (other.tag == "Spell")
        {
            damage = other.gameObject.GetComponent<SpellDamage>().spellDamage;
            CmdDealDamage(damage);
            Destroy(other.gameObject);
        }
    }

    #endregion

    //TODO: Raczej trzeba przerobić to na skrypt, który rejestruje obrażenia po stronie servera.
    //Również nie jest to ostateczna wersja tylko wczesne testy.
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            damage = other.gameObject.GetComponent<SpellDamage>().spellDamage;
            CmdDealDamage(damage);
            Destroy(other.gameObject);
        }
    }
    */
}
