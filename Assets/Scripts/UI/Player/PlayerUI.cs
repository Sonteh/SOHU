using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Fireball fireball;
    [SerializeField] private Image fireballImage;
    private float fireballCooldown = 2.0f;
    private bool coolingDown = false;
    private bool healthChanged = false;
    private float currentHealth;

    void Update()
    {
        if (!isLocalPlayer) {return;}

        // if (healthChanged)
        // {
        //     healthBarImage.fillAmount = currentHealth / 100;
        //     healthChanged = false;
        // }

        if ((Input.GetButtonDown("Fireball")) && fireballImage.fillAmount == 1.0f )
        {
            fireballImage.fillAmount = 0f;
            coolingDown = true;
        }
        
        Debug.Log("CoolingDown = " + coolingDown);
        if (coolingDown)
        {
            Debug.Log("FILLING");
            fireballImage.fillAmount += 1.0f / fireballCooldown * Time.deltaTime;

            if (fireballImage.fillAmount >= 1.0f)
            {
                Debug.Log("FILLED");
                coolingDown = false;
            }
        }
    }

    // public void PlayerHealthBar(float health)
    // {
    //     currentHealth = health;
    //     Debug.Log("Health: " + currentHealth);
    //     healthChanged = true;
    // }
}
