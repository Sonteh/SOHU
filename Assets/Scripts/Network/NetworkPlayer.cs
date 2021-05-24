using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";
    public int playerScore = 0;

    [SerializeField] RoundSystem roundSystem;

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

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Server]
    public void IncrementPlayerScore()
    {
    
        playerScore++;
        Debug.Log("Player Score: " + displayName + " " + playerScore);
    }

    [Server]
    public void PlayerDeath()
    {
        //Room.GamePlayers.Remove(this);
        GameObject testObject = GameObject.Find("RoundSystem(Clone)");
        Debug.Log("Testowe remaing players from Network Player before: " + testObject.GetComponent<RoundSystem>().remainingPlayers.Count);
        //testObject.GetComponent<RoundSystem>().remainingPlayers.Remove(gameObject);
        Debug.Log("Testowe remaing players from Network Player after: " + testObject.GetComponent<RoundSystem>().remainingPlayers.Count);
        //Debug.Log("Remaing players from Network Player before delete: " + roundSystem.remainingPlayers.Count);
        //roundSystem.remainingPlayers.Remove(this);
        //Debug.Log("Remaing players from Network Player: " + roundSystem.remainingPlayers.Count);
        //Debug.Log("Remaing players from Network Player before delete: " + testObject.GetComponent<RoundSystem>().remainingPlayers.Count);
        //testObject.GetComponent<RoundSystem>().remainingPlayers.Remove(Room.Pla);
        //Debug.Log("Remaing players from Network Player after delete: " + testObject.GetComponent<RoundSystem>().remainingPlayers.Count);
    }

}
