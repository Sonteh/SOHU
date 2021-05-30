using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    public string displayName = "Loading...";
    [SyncVar]
    public int playerScore = 0;
    public bool IsShopTime = false;

    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private Health health;
    [SerializeField] private PlayerShop playerShop;
    //[SerializeField] private GameObject playerShopObject;
    public bool isDead;

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
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Server]
    public void IncrementPlayerScore()
    {
        playerScore++;
        Debug.Log("Player " + displayName + " Score: " + playerScore);
    }

    //[Server]
    public void ShowPlayerShop()
    {
        //if (!isLocalPlayer) {return;}
        //playerShopObject.SetActive(true);
        //if (!hasAuthority) {return;}
        //Debug.Log("Hello from authority");
        //CmdShowPlayerShop();
        IsShopTime = !IsShopTime;
        Debug.Log("Player shop from Netowrk Player " + IsShopTime);
    }

    [Command]
    private void CmdShowPlayerShop()
    {
        RpcShowPlayerShop();
    }

    [ClientRpc]
    private void RpcShowPlayerShop()
    {
        playerShop.ShowPlayerShop();
    }
}
