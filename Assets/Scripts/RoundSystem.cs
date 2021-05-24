using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundSystem : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    public int numberOfPlayersAlive;
    //public List<GameObject> remainingPlayers;
    public List<NetworkPlayer> remainingPlayers;
    [SerializeField] private NetworkPlayer networkPlayer;
    private Network room;
    private Network Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as Network;
        }
    }

     public void CountdownEnded()
    {
        animator.enabled = false;
    }

    #region Server

    public override void OnStartServer()
    {
        Network.OnServerStopped += CleanUpServer;
        Network.OnServerReadied += CheckToStartRound;
    }

    [ServerCallback]
    private void OnDestroy() => CleanUpServer();

    [Server]
    private void CleanUpServer()
    {
        Network.OnServerStopped -= CleanUpServer;
        Network.OnServerReadied -= CheckToStartRound;
    }
    
    [ServerCallback]
    public void StartRound()
    {
        RpcStartRound();
    }

    //TODO: Naprawić wyświetlanie się canvasu GameMenu w tym skrypcie, jak i w Network.cs
    [Server]
    private void CheckToStartRound(NetworkConnection conn)
    {
        //Debug.Log("Liczba graczy: " + Room.numPlayers);
        if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count) {return;}

        animator.enabled = true;

        remainingPlayers = Room.GamePlayers.ToList();
        Debug.Log("Remaing Players: " + String.Join(",", remainingPlayers));
        //var myObject = GameObject.FindGameObjectsWithTag("Player");
       // for (var i=0; i < myObject.Length; i++)
        //{
       //     this.remainingPlayers.Add(myObject[i]);
       //     Debug.Log("Testowy for: " + remainingPlayers.Count());
       // } 
       foreach (var testPlayer in remainingPlayers)
       {
           Debug.Log("Player score:" + testPlayer.playerScore);
       }

        RpcStartCountdown();
        //numberOfPlayersAlive = Room.numPlayers;
    }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcStartCountdown()
    {
        animator.enabled = true;
        numberOfPlayersAlive = Room.numPlayers;
    }

    [ClientRpc]
    private void RpcStartRound()
    {
        Debug.Log("Start");
    }
    
    #endregion

    //TODO: Skończyć poprawne uruchamianie rundy
    [Server]
    public void OnDeath()
    {
        numberOfPlayersAlive--;
        Debug.Log("Testowe1 remaing players: " + remainingPlayers.Count());
        //remainingPlayers.Remove(gameObject);
        //remainingPlayers.Remove(networkPlayer);
        
        foreach (var player in remainingPlayers)
        {
            if (player.connectionToClient == connectionToClient)
            {
                remainingPlayers.Remove(player);
                break;
            }
        }
            
        Debug.Log("Testowe2 remaing players: " + remainingPlayers.Count());
        Debug.Log("Remaing Players: " + String.Join(",", remainingPlayers));
        //remainingPlayers.Remove(this);
        //Room.GamePlayers.Remove(this);
        //Debug.Log("Remaing players before Player Death: " + remainingPlayers.Count());
        
        //networkPlayer.PlayerDeath();
        //remainingPlayers.Remove(networkPlayer.netIdentity.connectionToClient);
        //Debug.Log("Remaing players after Player Death: " + remainingPlayers.Count());
        //remainingPlayers[this].
        Debug.Log("OnDeath :" + numberOfPlayersAlive);
        if (numberOfPlayersAlive == 1)
        {
            //Debug.Log("From OnDeath IF: " + Room.GamePlayers[0].connectionToClient);
            HandleRoundEnd();
            //Room.OnRoundEnd(Room.GamePlayers.);
            //remainingPlayers[0].IncrementPlayerScore();
            //Debug.Log("Player score: " + remainingPlayers[0].playerScore);
            //Room.StartGame();
        }
    }

    [Server]
    private void HandleRoundEnd()
    {
        remainingPlayers[0].IncrementPlayerScore();
        Debug.Log("Player score: " + remainingPlayers[0].playerScore);
        Room.StartGame();
    }

    /*

    [Command]
    private void HandleRoundEnd()
    {
        //SceneManager.LoadScene("Arena01");
        numberOfPlayersAlive = Room.numPlayers;
        Room.StartGame();
    }
    */
}
