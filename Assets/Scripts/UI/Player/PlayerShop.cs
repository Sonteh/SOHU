using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerShop : NetworkBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private TMP_Text playerGold;
    [SerializeField] public TMP_Text timeText;
    private float timeRemaining = 16;

    #region Button References
    
    [Header("Buy Buttons")]
    [SerializeField] private Button buyMagicMissleButton;
    [SerializeField] private Button buyRollingMeteorButton;
    [SerializeField] private Button buyPortableZoneButton;
    [SerializeField] private Button buyTacticalRecallButton;
    [SerializeField] private Button buyHealButton;
    [SerializeField] private Button buyHealZoneButton;

    [Header("Sell Buttons")]
    [SerializeField] private Button sellMagicMissleButton;
    [SerializeField] private Button sellRollingMeteorButton;
    [SerializeField] private Button sellPortableZoneButton;
    [SerializeField] private Button sellTacticalRecallButton;
    [SerializeField] private Button sellHealButton;
    [SerializeField] private Button sellHealZoneButton;

    [Header("Upgrade Buttons")]
    [SerializeField] private Button upgradeMagicMissleButton;
    [SerializeField] private Button upgradeRollingMeteorButton;
    [SerializeField] private Button upgradePortableZoneButton;
    [SerializeField] private Button upgradeTacticalRecallButton;
    [SerializeField] private Button upgradeHealButton;
    [SerializeField] private Button upgradeHealZoneButton;

    #endregion

    private void Update() 
    {
        if (!isLocalPlayer) {return;}
        
        if (networkPlayer.IsShopTime)
        {
            ShowPlayerShop();

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                DisplayTime(timeRemaining);
            }
        }
        else
        {
            shopUI.SetActive(false);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.SetText(seconds.ToString());
    }

    public void ShowPlayerShop()
    { 
        playerGold.SetText(networkPlayer.playerGold.ToString());
        shopUI.SetActive(true);
    }

    public void SpellBuyButtonName()
    {
        string nameOfBoughtSpell = EventSystem.current.currentSelectedGameObject.name;

        if(nameOfBoughtSpell == "MagicMissleBuyButton" && networkPlayer.playerGold >= 50)
        {   
            buyMagicMissleButton.interactable = false; 
            sellMagicMissleButton.interactable = true;
            upgradeMagicMissleButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "MeteorBuyButton" && networkPlayer.playerGold >= 100)
        {
            buyRollingMeteorButton.interactable = false;
            sellRollingMeteorButton.interactable = true;
            upgradeRollingMeteorButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "PortableZoneBuyButton" && networkPlayer.playerGold >= 100)
        {
            buyPortableZoneButton.interactable = false;
            sellPortableZoneButton.interactable = true;
            upgradePortableZoneButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "RecallBuyButton" && networkPlayer.playerGold >= 50)
        {
            buyTacticalRecallButton.interactable = false;
            sellTacticalRecallButton.interactable = true;
            upgradeTacticalRecallButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "HealBuyButton" && networkPlayer.playerGold >= 100)
        {
            buyHealButton.interactable = false;
            sellHealButton.interactable = true;
            upgradeHealButton.interactable = true;
            CmdBuySpell(nameOfBoughtSpell);
        }

        if(nameOfBoughtSpell == "HealZoneBuyButton" && networkPlayer.playerGold >= 50)
        {
            buyHealZoneButton.interactable = false;
            sellHealZoneButton.interactable = true;
            upgradeHealZoneButton.interactable = true;
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
            upgradeMagicMissleButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        if (nameOfSoldSpell == "MeteorSellButton" && networkPlayer.playerScript.IsMeteorBought)
        {
            sellRollingMeteorButton.interactable = false;
            buyRollingMeteorButton.interactable = true;
            upgradeRollingMeteorButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        if (nameOfSoldSpell == "PortableZoneSellButton" && networkPlayer.playerScript.IsPortableZoneBought)
        {
            sellPortableZoneButton.interactable = false;
            buyPortableZoneButton.interactable = true;
            upgradePortableZoneButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        if (nameOfSoldSpell == "RecallSellButton" && networkPlayer.playerScript.IsRecallBought)
        {
            sellTacticalRecallButton.interactable = false;
            buyTacticalRecallButton.interactable = true;
            upgradeTacticalRecallButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        if(nameOfSoldSpell == "HealSellButton" && networkPlayer.playerScript.IsHealBought)
        {
            sellHealButton.interactable = false;
            buyHealButton.interactable = true;
            upgradeHealButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }

        if(nameOfSoldSpell == "HealZoneSellButton" && networkPlayer.playerScript.IsHealZoneBought)
        {
            sellHealZoneButton.interactable = false;
            buyHealZoneButton.interactable = true;
            upgradeHealZoneButton.interactable = true;
            CmdSellSpell(nameOfSoldSpell);
        }
    }

    [Command]
    private void CmdSellSpell(string nameOfSoldSpell)
    {
        networkPlayer.PlayerSoldSpell(nameOfSoldSpell);
    }

    public void SpellUpgradeButtonName()
    {
        string nameOfUpgradeSpell = EventSystem.current.currentSelectedGameObject.name;

        if (nameOfUpgradeSpell == "MagicMissleUpgradeButton" && networkPlayer.playerScript.IsMagicMissleBought && networkPlayer.playerGold >= 50)
        {
            upgradeMagicMissleButton.interactable = false;
            CmdUpgradeSpell(nameOfUpgradeSpell);
        }

        if (nameOfUpgradeSpell == "MeteorUpgradeButton" && networkPlayer.playerScript.IsMeteorBought && networkPlayer.playerGold >= 100)
        {
            upgradeRollingMeteorButton.interactable = false;
            CmdUpgradeSpell(nameOfUpgradeSpell);
        }

        if (nameOfUpgradeSpell == "PortableZoneUpgradeButton" && networkPlayer.playerScript.IsPortableZoneBought && networkPlayer.playerGold >= 75)
        {
            upgradePortableZoneButton.interactable = false;
            CmdUpgradeSpell(nameOfUpgradeSpell);
        }

        if (nameOfUpgradeSpell == "RecallUpgradeButton" && networkPlayer.playerScript.IsRecallBought && networkPlayer.playerGold >= 25)
        {
            upgradeTacticalRecallButton.interactable = false;
            CmdUpgradeSpell(nameOfUpgradeSpell);
        }

        if(nameOfUpgradeSpell == "HealUpgradeButton" && networkPlayer.playerScript.IsHealBought && networkPlayer.playerGold >= 75)
        {
            upgradeHealButton.interactable = false;
            CmdUpgradeSpell(nameOfUpgradeSpell);
        }

        if(nameOfUpgradeSpell == "HealZoneUpgradeButton" && networkPlayer.playerScript.IsHealZoneBought && networkPlayer.playerGold >= 25)
        {
            upgradeHealZoneButton.interactable = false;
            CmdUpgradeSpell(nameOfUpgradeSpell);
        }
    }

    [Command]
    public void CmdUpgradeSpell(string nameOfUpgradeSpell)
    {
        networkPlayer.PlayerUpgradeSpell(nameOfUpgradeSpell);
    }
}
