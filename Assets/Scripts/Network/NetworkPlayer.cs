using Mirror;
using UnityEngine;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;
    [SerializeField] public Player playerScript;

    [Header("Spell Scripts")]
    [SerializeField] private SpellData magicMissleData;
    [SerializeField] private MagicMissle magicMissleScript;
    [SerializeField] private RollingMeteor rollingMeteorScript;
    [SerializeField] private PortableZone portableZoneScript;
    [SerializeField] private TacticalRecall tacticalRecallScript;
    [SerializeField] private QuickHeal quickHealScript;
    [SerializeField] private HealingZone healingZoneScript;

    [Header("")]
    public UIScript uiScript;
    [SyncVar]
    public string displayName = "Loading...";
    [SyncVar]
    public int playerScore = 0;
    [SyncVar]
    public bool IsShopTime = false;
    [SyncVar]
    public int playerGold = 0;

    private Network room;
    private Network Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as Network;
        }
    }

    private void Start() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    private void Update() 
    {
        if (!isLocalPlayer) { return; }

        if (uiScript == null)
        {
            uiScript = GameObject.FindObjectOfType<UIScript>();
        }
        
        if (uiScript != null)
        {
            uiScript.playerGold.SetText(playerGold.ToString());
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    public override void OnStartAuthority()
    {
        SetDisplayName(PlayerPrefs.GetString("PlayerName"));
        PreparePlayerSpells();
    }

    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Server]
    public void IncrementPlayerScore()
    {
        playerScore++;
    }

    [Server]
    public void GivePlayerGold(int goldAmount)
    {
        playerGold += goldAmount;
    }

    [Server]
    private void TakePlayerGold(int goldAmount)
    {
        playerGold -= goldAmount;
    }

    private void PreparePlayerSpells()
    {
        playerScript.IsMagicMissleBought = false;
        magicMissleScript.magicMissleCooldown = 5f;
        magicMissleScript.speed = 25f;
        
        playerScript.IsMeteorBought = false;
        rollingMeteorScript.rollingMeteorCooldown = 10f;
        rollingMeteorScript.speed = 15f;

        playerScript.IsPortableZoneBought = false;
        portableZoneScript.portableZoneCooldown = 8f;

        playerScript.IsRecallBought = false;
        tacticalRecallScript.tacticalRecallCooldown = 15f;

        playerScript.IsHealBought = false;
        quickHealScript.quickHealCooldown = 12f;

        playerScript.IsHealZoneBought = false;
        healingZoneScript.healingZoneCooldown = 8f;
    }

    [Server]
    public void PlayerBoughtSpell(string spellBought)
    {
        if (spellBought == "MagicMissleBuyButton")
        {
            TakePlayerGold(50);
            TargetMagicMissleBought();
        }

        if (spellBought == "MeteorBuyButton")
        {
            TakePlayerGold(100);
            TargetMeteorBought();
        }

        if (spellBought == "PortableZoneBuyButton")
        {
            TakePlayerGold(100);
            TargetPortableZoneBought();
        }

        if (spellBought == "RecallBuyButton")
        {
            TakePlayerGold(50);
            TargetRecallBought();
        }

        if (spellBought == "HealBuyButton")
        {
            TakePlayerGold(100);
            TargetHealBought();
        }

        if (spellBought == "HealZoneBuyButton")
        {
            TakePlayerGold(50);
            TargetHealZoneBought();
        }
    }

    //If the first parameter of your TargetRpc method is a NetworkConnection then that's the connection that will receive the message regardless of context.
    //If the first parameter is any other type, then the owner client of the object with the script containing your TargetRpc will receive the message.
    [TargetRpc]
    public void TargetMagicMissleBought()
    {
        playerScript.IsMagicMissleBought = true;
    }

    [TargetRpc]
    public void TargetMeteorBought()
    {
        playerScript.IsMeteorBought = true;
    }

    [TargetRpc]
    public void TargetPortableZoneBought()
    {
        playerScript.IsPortableZoneBought = true;
    }

    [TargetRpc]
    public void TargetRecallBought()
    {
        playerScript.IsRecallBought = true;
    }

    [TargetRpc]
    public void TargetHealBought()
    {
        playerScript.IsHealBought = true;
    }

    [TargetRpc]
    public void TargetHealZoneBought()
    {
        playerScript.IsHealZoneBought = true;
    }

    [Server]
    public void PlayerSoldSpell(string spellSold)
    {
        if (spellSold == "MagicMissleSellButton")
        {
            GivePlayerGold(50);
            TargetMagicMissleSold();
        }

        if (spellSold == "MeteorSellButton")
        {
            GivePlayerGold(100);
            TargetMeteorSold();
        }

        if (spellSold == "PortableZoneSellButton")
        {
            GivePlayerGold(100);
            TargetPortableZoneSold();
        }

        if (spellSold == "RecallSellButton")
        {
            GivePlayerGold(50);
            TargetRecallSold();
        }

        if (spellSold == "HealSellButton")
        {
            GivePlayerGold(100);
            TargetHealSold();
        }

        if (spellSold == "HealZoneSellButton")
        {
            GivePlayerGold(50);
            TargetHealZoneSold();
        }
    }

    [TargetRpc]
    public void TargetMagicMissleSold()
    {
        playerScript.IsMagicMissleBought = false;
    }

    [TargetRpc]
    public void TargetMeteorSold()
    {
        playerScript.IsMeteorBought = false;
    }

    [TargetRpc]
    public void TargetPortableZoneSold()
    {
        playerScript.IsPortableZoneBought = false;
    }

    [TargetRpc]
    public void TargetRecallSold()
    {
        playerScript.IsRecallBought = false;
    }

    [TargetRpc]
    public void TargetHealSold()
    {
        playerScript.IsHealBought = false;
    }

    [TargetRpc]
    public void TargetHealZoneSold()
    {
        playerScript.IsHealZoneBought = false;
    }

    [Server]
    public void PlayerUpgradeSpell(string spellUpgrade)
    {
        if (spellUpgrade == "MagicMissleUpgradeButton")
        {
            TakePlayerGold(50);
            TargetMagicMissleUpgrade();
        }

        if (spellUpgrade == "MeteorUpgradeButton")
        {
            TakePlayerGold(100);
            TargetMeteorUpgrade();
        }

        if (spellUpgrade == "PortableZoneUpgradeButton")
        {
            TakePlayerGold(75);
            TargetPortableZoneUpgrade();
        }

        if (spellUpgrade == "RecallUpgradeButton")
        {
            TakePlayerGold(25);
            TargetRecallUpgrade();
        }

        if (spellUpgrade == "HealUpgradeButton")
        {
            TakePlayerGold(75);
            TargetHealUpgrade();
        }

        if (spellUpgrade == "HealZoneUpgradeButton")
        {
            TakePlayerGold(25);
            TargetHealZoneUpgrade();
        }
    }

    [TargetRpc]
    public void TargetMagicMissleUpgrade()
    {
        magicMissleScript.magicMissleCooldown *= 0.5f;
        magicMissleScript.speed *= 1.50f;
    }

    [TargetRpc]
    public void TargetMeteorUpgrade()
    {
        rollingMeteorScript.rollingMeteorCooldown *= 0.75f;
        rollingMeteorScript.speed *= 1.25f;
    }

    [TargetRpc]
    public void TargetPortableZoneUpgrade()
    {
        portableZoneScript.portableZoneCooldown *= 0.5f;
    }

    [TargetRpc]
    public void TargetRecallUpgrade()
    {
        tacticalRecallScript.tacticalRecallCooldown *= 0.75f;
    }

    [TargetRpc]
    public void TargetHealUpgrade()
    {
        quickHealScript.quickHealCooldown *= 0.75f;
    }

    [TargetRpc]
    public void TargetHealZoneUpgrade()
    {
        healingZoneScript.healingZoneCooldown *= 0.5f;
    }
}