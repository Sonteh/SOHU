using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private Health health;
    [SyncVar]
    public string displayName = "Loading...";
    [SyncVar]
    public int playerScore = 0;
    [SyncVar]
    public bool IsShopTime = false;

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
    }
}
