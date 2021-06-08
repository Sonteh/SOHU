using Mirror;
using UnityEngine;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private Player playerScript;
    [SerializeField] private GameObject playerUIObject;
    [SerializeField] private PlayerUI playerUI;
 
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

    public override void OnStartClient()
    {
        Debug.Log("Game Player Add from OnStartClient");
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

        //NetworkServer.Spawn(playerUIObject, connectionToClient);
        playerUIObject.SetActive(true);
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
            TargetMagicMissleBought();
        }

        if (spellBought == "MeteorBuyButton")
        {
            TargetMeteorBought();
        }

        if (spellBought == "PortableZoneBuyButton")
        {
            TargetPortableZoneBought();
        }

        if (spellBought == "RecallBuyButton")
        {
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
}