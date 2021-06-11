using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerShop : NetworkBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private NetworkPlayer networkPlayer;

    [Header("Buy Buttons")]
    [SerializeField] private Button buyMagicMissleButton;
    [SerializeField] private Button buyRollingMeteorButton;
    [SerializeField] private Button buyPortableZoneButton;
    [SerializeField] private Button buyTacticalRecallButton;

    [Header("Sell Buttons")]
    [SerializeField] private Button sellMagicMissleButton;
    [SerializeField] private Button sellRollingMeteorButton;
    [SerializeField] private Button sellPortableZoneButton;
    [SerializeField] private Button sellTacticalRecallButton;

    [Header("Upgrade Buttons")]
    [SerializeField] private Button upgradeMagicMissleButton;
    [SerializeField] private Button upgradeRollingMeteorButton;
    [SerializeField] private Button upgradePortableZoneButton;
    [SerializeField] private Button upgradeTacticalRecallButton;

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
            buyMagicMissleButton.interactable = false; 
            sellMagicMissleButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "MeteorBuyButton" && networkPlayer.playerGold >= 100)
        {
            buyRollingMeteorButton.interactable = false;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "PortableZoneBuyButton" && networkPlayer.playerGold >= 100)
        {
            buyPortableZoneButton.interactable = false;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "RecallBuyButton" && networkPlayer.playerGold >= 50)
        {
            buyTacticalRecallButton.interactable = false;
            CmdBuySpell(nameOfBoughtSpell);
        }
    }

    [Command]
    private void CmdBuySpell(string nameOfBoughtSpell)
    {
        networkPlayer.PlayerBoughtSpell(nameOfBoughtSpell);
    }

    public void SpellSellButtonName()
    {
        string nameOfSoldSpell = EventSystem.current.currentSelectedGameObject.name;

        if (nameOfSoldSpell == "MagicMissleSellButton" && networkPlayer.playerScript.IsMagicMissleBought)
        {
            sellMagicMissleButton.interactable = false;
            buyMagicMissleButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }
    }

    [Command]
    private void CmdSellSpell(string nameOfSoldSpell)
    {
        networkPlayer.PlayerSoldSpell(nameOfSoldSpell);
    }
}
