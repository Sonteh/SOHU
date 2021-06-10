using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class UIScript : NetworkBehaviour
{
    public Health health;
    public Image imageHealthBar;

    //[SyncVar(hook = nameof(OnPlayerHealthChanged))]
    public float playerHealth;

    private void OnPlayerHealthChanged(float _old, float _new)
    {
        Debug.Log("PLAYER HEALTH: " + playerHealth);
        imageHealthBar.fillAmount = playerHealth / 100;
    }

    public void PlayerHealth(float _value)
    {
         imageHealthBar.fillAmount = _value / 100;
    }
}
