using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HealthDisplay : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private Image healthBarImage;

    private void OnEnable() 
    {
        health.EventHealthUpdate += HandleHealthChanged;                
    }

    private void OnDisable() 
    {
        health.EventHealthUpdate += HandleHealthChanged;
    }

    [ClientRpc]
    private void HandleHealthChanged(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }
}
