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
        if (!hasAuthority) {return;}

        if (networkPlayer.IsShopTime)
        {
            //Debug.Log("Player shop from IF " + networkPlayer.IsShopTime);
            //ShowPlayerShop();
            shopUI.SetActive(true);
        }
        else
        {
          shopUI.SetActive(false);  
        }

        if (Input.GetKey(KeyCode.P))
        {
            //shopUI.SetActive(true);
            ShowPlayerShop();
        }
        else
        {
          //shopUI.SetActive(false);  
        }
    }

    public void ShowPlayerShop()
    {
        //if (!isLocalPlayer) {return;}
        
        shopUI.SetActive(true);
    }
}
