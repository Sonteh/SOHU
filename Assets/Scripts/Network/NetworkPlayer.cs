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

    [SerializeField] RoundSystem roundSystem;
    [SerializeField] GameObject player;
    [SerializeField] Health health;
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

/*
    [Server]
    public void PlayerDeath(bool died)
    {
        //if(!isLocalPlayer) {return;}
        
        Debug.Log("Before PlayerDeath(): " + isDead);

        isDead = died;
        Debug.Log("After PlayerDeath(): " + isDead);
    }
    */
}
