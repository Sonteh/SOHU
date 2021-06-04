using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private Fireball fireball;
    [SerializeField] private Image fireballImage;
    private float fireballCooldown = 2.0f;
    private bool coolingDown = false;

    void Update()
    {
        if (!isLocalPlayer) {return;}

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
}
