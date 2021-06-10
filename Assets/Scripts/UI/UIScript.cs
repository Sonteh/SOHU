using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class UIScript : NetworkBehaviour
{
    public Health health;
    public Image imageHealthBar;
    public float playerHealth;

    public Fireball fireball;
    public Image fireballImage;
    public float fireballCooldownTime;
    public bool fireballCooldown;

    public void PlayerHealth(float _value)
    {
         imageHealthBar.fillAmount = _value / 100;
    }

    private void Update() 
    {
        if (fireballCooldown && fireballImage.fillAmount == 1.0f )
        {
            fireballImage.fillAmount = 0f;
        }
        
        if (fireballCooldown)
        {
            fireballImage.fillAmount += 1.0f / fireballCooldownTime * Time.deltaTime;

            if (fireballImage.fillAmount >= 1.0f)
            {
                fireballCooldown = false;
            }
        }
    }
}
