using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShop : NetworkBehaviour
{
    [SerializeField] private GameObject shopUI;

    private void Update() 
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetKeyDown(KeyCode.P))
        {
            //shopUI.SetActive(true);
            ShowPlayerShop();
        }
    }

    public void ShowPlayerShop()
    {
        if (!isLocalPlayer) {return;}
        Debug.Log("Test aktywacji z PlayerShop");
        shopUI.SetActive(true);
    }
}
