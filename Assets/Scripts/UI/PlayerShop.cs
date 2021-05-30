using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShop : NetworkBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private NetworkPlayer networkPlayer;

    private void Update() 
    {
        if (!isLocalPlayer) {return;}
        
        if (networkPlayer.IsShopTime == true)
        {
            shopUI.SetActive(true);
        }
        else
        {
            shopUI.SetActive(false);
        }

        if (Input.GetKey(KeyCode.P))
        {
            ShowPlayerShop();
        }

    }

    public void ShowPlayerShop()
    {    
        shopUI.SetActive(true);
    }
}
