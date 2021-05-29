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
    //[SerializeField] private GameObject shopUI;
    [SerializeField] private PlayerShop playerShop;
    [SerializeField] public int scoreToWin = 3;
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
        if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count) {return;}

        animator.enabled = true;

        remainingPlayers = Room.GamePlayers.ToList();
        Debug.Log("Created Player List: " + String.Join(",", remainingPlayers));
        //var myObject = GameObject.FindGameObjectsWithTag("Player");
       // for (var i=0; i < myObject.Length; i++)
        //{
       //     this.remainingPlayers.Add(myObject[i]);
       //     Debug.Log("Testowy for: " + remainingPlayers.Count());
       // } 
        foreach (var testPlayer in remainingPlayers)
        {
           Debug.Log("Player " + testPlayer.netId + " score: " + testPlayer.playerScore);
        }

        RpcStartCountdown();
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
    public void OnDeath(NetworkConnection connectionToClient)
    {
        //remainingPlayers = Room.GamePlayers.ToList();
        Debug.Log("Created Player List: " + String.Join(",", remainingPlayers));
        numberOfPlayersAlive--;
        //networkPlayer.PlayerDeath();
        Debug.Log("Amount of players before OnDeath Remove: " + remainingPlayers.Count());
        
        foreach (var player in remainingPlayers)
        {
            if (player.connectionToClient == connectionToClient)
            {
                remainingPlayers.Remove(player);
                break;
            }
        }
            
        Debug.Log("Amount of players after OnDeath Remove:" + remainingPlayers.Count());
        foreach (var testPlayer in remainingPlayers)
       {
           Debug.Log("Player netId " + testPlayer.netId + " isDead " + testPlayer.isDead);
       }

        //Debug.Log("OnDeath :" + numberOfPlayersAlive);
        if (numberOfPlayersAlive == 1)
        {
            if (remainingPlayers[0].playerScore == scoreToWin)
            {
                HandleGameEnd();
            }

            HandleRoundEnd();
        }
    }
/*
    [Server]
    private void HandleDeath(object sender, DeathEventArgs e) 
    {
        Debug.Log("RemaingPlayers Count: " + remainingPlayers.Count);
        if (remainingPlayers.Count == 1) {return;}

        foreach (var player in remainingPlayers)
        {
            if(player == null || player.connectionToClient == e.connectionToClient)
            {
                remainingPlayers.Remove(player);
                break;
            }
        }

        if (remainingPlayers.Count != 1) {return;}

        HandleRoundEnd();
    }
    */

    [Server]
    private void HandleGameEnd()
    {
        Debug.Log("Player " + remainingPlayers[0].displayName + " WON!");
    }

    [Server]
    private void HandleRoundEnd()
    {
        remainingPlayers[0].IncrementPlayerScore();
        networkPlayer.ShowPlayerShop();
        Debug.Log("Player netId " + remainingPlayers[0].netId + " score: " + remainingPlayers[0].playerScore);
        StartCoroutine(ShowShop());
        //CmdStartCoroutine();
        //Room.StartGame();
    }

    [Command]
    private void CmdStartCoroutine()
    {
        StartCoroutine(ShowShop());
    }
    private IEnumerator ShowShop()
    {
        //shopUI.SetActive(true);
        //playerShop.ShowPlayerShop();
        //networkPlayer.ShowPlayerShop();
        yield return new WaitForSecondsRealtime(5);
        //shopUI.SetActive(false);
        Room.StartGame();
    }
}
