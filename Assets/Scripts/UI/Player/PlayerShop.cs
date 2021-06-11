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

    #region Button References
    
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

    #endregion

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
            sellRollingMeteorButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "PortableZoneBuyButton" && networkPlayer.playerGold >= 100)
        {
            buyPortableZoneButton.interactable = false;
            sellPortableZoneButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "RecallBuyButton" && networkPlayer.playerGold >= 50)
        {
            buyTacticalRecallButton.interactable = false;
            //sellMagicMissleButton.interactable = true;
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

        if (nameOfSoldSpell == "MeteorSellButton" && networkPlayer.playerScript.IsMeteorBought)
        {
            sellRollingMeteorButton.interactable = false;
            buyRollingMeteorButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        if (nameOfSoldSpell == "PortableZoneSellButton" && networkPlayer.playerScript.IsPortableZoneBought)
        {
            sellPortableZoneButton.interactable = false;
            buyPortableZoneButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        // if (nameOfSoldSpell == "MagicMissleSellButton" && networkPlayer.playerScript.IsMagicMissleBought)
        // {
        //     sellMagicMissleButton.interactable = false;
        //     buyMagicMissleButton.interactable = true;
        //     CmdSellSpell(nameOfSoldSpell);
        // }
    }

    [Command]
    private void CmdSellSpell(string nameOfSoldSpell)
    {
        networkPlayer.PlayerSoldSpell(nameOfSoldSpell);
    }
}
