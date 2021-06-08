using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private Health health;
    [SerializeField] public Image healthBarImage;
    [SerializeField] private Fireball fireball;
    [SerializeField] private Image fireballImage;
    private float fireballCooldown = 1.0f;
    private bool coolingDown = false;

    private Network room;
    private Network Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as Network;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if ((Input.GetButtonDown("Fireball")) && fireballImage.fillAmount == 1.0f )
        {
            fireballImage.fillAmount = 0f;
            coolingDown = true;
        }
        
        if (coolingDown)
        {
            fireballImage.fillAmount += 1.0f / fireballCooldown * Time.deltaTime;

            if (fireballImage.fillAmount >= 1.0f)
            {
                coolingDown = false;
            }
        }
    }
}