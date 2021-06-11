using Mirror;
using UnityEngine;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;
    [SerializeField] public Player playerScript;
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
        //playerUI.playerGoldAmount = 0;
        PreparePlayerSpells();

        //NetworkServer.Spawn(playerUIObject, connectionToClient);
        //playerUIObject.SetActive(true);
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
        //uiScript.UpdatePlayerInfo(playerGold);
        //playerUI.playerGoldAmount = playerGold;
    }

    [Server]
    private void TakePlayerGold(int goldAmount)
    {
        playerGold -= goldAmount;
        //uiScript.UpdatePlayerInfo(playerGold);
        //playerUI.playerGoldAmount = playerGold;
    }

    // private void HandleGoldChanged(int _old, int _new)
    // {
    //     uiScript.playerGold.SetText(playerGold.ToString());
    // }

    private void PreparePlayerSpells()
    {
        playerScript.IsMeteorBought = false;
        playerScript.IsMagicMissleBought = false;
        playerScript.IsPortableZoneBought = false;
        playerScript.IsRecallBought = false;
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

        // if (spellSold == "RecallSellButton")
        // {
        //     GivePlayerGold(50);
        //     TargetRecallSold();
        // }
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
}