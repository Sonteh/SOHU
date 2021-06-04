using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private Fireball fireball;
    [SerializeField] private Image fireballImage;
    private float fireballCooldown = 2.0f;
    private bool coolingDown = false;

    void Start()
    {
        //Debug.Log("Fireball cooldown from PlayerUI: " + fireballCooldown);
        //fireballImage.fillAmount = 1.0f;

    }

    void Update()
    {
        if (!isLocalPlayer) {return;}

        // if (fireballImage.fillAmount < 1 )
        // {
        //     Debug.Log("Fill amount: " + fireballCooldown / 100000f);
        //     fireballImage.fillAmount = fireballCooldown / 100000f;
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

    public void UpdateFireball(float fill)
    {
        // fireballCooldown = testowyFireball;
        // Debug.Log("Fireball cooldown from PlayerUI: " + fireballCooldown);
        // //coolingDown = true;
        // Debug.Log(coolingDown);
        fireballImage.fillAmount = fill;
    }
}
