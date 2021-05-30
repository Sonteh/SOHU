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
        
        Debug.Log("Test i chuj przed ifem razem z IsShopTime " + networkPlayer.IsShopTime);
        if (networkPlayer.IsShopTime == true)
        {
            Debug.Log("Test i chuj po razem z IsShopTime " + networkPlayer.IsShopTime);
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

    }

    public void ShowPlayerShop()
    {
        //if (!isLocalPlayer) {return;}
        
        shopUI.SetActive(true);
    }

    // public void UpdateShop(bool IsShopTime)
    // {
    //     Debug.Log("Dajcie mi IsShopTime: " + IsShopTime);
    //     if (IsShopTime == true) 
    //     {
    //         shopUI.SetActive(true);
    //     } 
    // }
}
