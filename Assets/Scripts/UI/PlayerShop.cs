using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class PlayerShop : NetworkBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private NetworkPlayer networkPlayer;

    private void Update() 
    {
        if (!isLocalPlayer) {return;}
        
        if (networkPlayer.IsShopTime)
        {
            ShowPlayerShop();
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

    public void SpellBuyButtonName()
    {
        string nameOfBoughtSpell = EventSystem.current.currentSelectedGameObject.name;
        
        if(nameOfBoughtSpell == "MagicMissleBuyButton" && networkPlayer.playerGold >= 50)
        {
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "MeteorBuyButton" && networkPlayer.playerGold >= 100)
        {
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "PortableZoneBuyButton" && networkPlayer.playerGold >= 100)
        {
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "RecallBuyButton" && networkPlayer.playerGold >= 50)
        {
            CmdBuySpell(nameOfBoughtSpell);
        }
        //CmdBuySpell(nameOfBoughtSpell);
    }

    [Command]
    private void CmdBuySpell(string nameOfBoughtSpell)
    {
        networkPlayer.PlayerBoughtSpell(nameOfBoughtSpell);
    }
}
