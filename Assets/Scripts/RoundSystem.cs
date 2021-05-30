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
    [SerializeField] public PlayerShop playerShop;
    [SerializeField] private GameObject playerShopObject;
    [SerializeField] public int scoreToWin = 3;
    //[SyncVar]
    //public bool IsShopTime;
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
        foreach (var player in Room.GamePlayers)
        {
            player.IsShopTime = false;
        }
        
        animator.enabled = true;
        //IsShopTime = false;
        remainingPlayers = Room.GamePlayers.ToList();

        RpcStartCountdown();
    }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcStartCountdown()
    {
        animator.enabled = true;
    }

    [ClientRpc]
    private void RpcStartRound()
    {
        Debug.Log("Start");
    }
    
    #endregion

    [Server]
    public void OnDeath(NetworkConnection connectionToClient)
    {
        foreach (var player in remainingPlayers)
        {
            if (player.connectionToClient == connectionToClient)
            {
                remainingPlayers.Remove(player);
                break;
            }
        }
        
        if (remainingPlayers.Count == 1)
        {
            remainingPlayers[0].IncrementPlayerScore();

            foreach (var player in Room.GamePlayers)
            {
                //Debug.Log("Shop shop for before change " + player.displayName + " - " + player.IsShopTime);
                //player.ShowPlayerShop();
                //RpcChangeBool();
                player.IsShopTime = true;
                //Debug.Log("Shop shop for after change " + player.displayName + " - " + player.IsShopTime);
                if (player.playerScore == scoreToWin)
                {
                    HandleGameEnd();
                }
            }
            //TestChangeBool();
            HandleRoundEnd();
        }
    }

    [Server]
    private void HandleGameEnd()
    {
        Debug.Log("Player " + remainingPlayers[0].displayName + " WON!");
        Room.StopClient();
        Room.StopServer();
    }

    [Server]
    private void HandleRoundEnd()
    {
        //networkPlayer.ShowPlayerShop();
        // foreach (var player in Room.GamePlayers)
        // {
        //     Debug.Log("Shop shop for before " + player.displayName + " " + player.IsShopTime);
        //     player.ShowPlayerShop();
        //     Debug.Log("Shop shop for after " + player.displayName + " " + player.IsShopTime);
        // }
        // Debug.Log("IsShopTime from RoundSystem przed = " + IsShopTime);
        //CmdChangeBool();
        // Debug.Log("IsShopTime from RoundSystem po = " + IsShopTime);
        //playerShop.UpdateShop(IsShopTime);
        //TestChangeBool();
        StartCoroutine(ShowShop());
    }

    // [Command]
    // public void CmdChangeBool()
    // {
    //     IsShopTime = !IsShopTime;
    // }
    // [Server]
    // public void TestChangeBool()
    // {
    //     Debug.Log("Kurwo działaj");
    //     Debug.Log("IsShopTime przed: " + IsShopTime);
    //     IsShopTime = !IsShopTime;
    //     Debug.Log("IsShopTime po: " + IsShopTime);
    // }

    // [Command]
    // public void CmdChangeBool()
    // {
    //     IsShopTime = !IsShopTime;
    // }

    [ClientRpc]
    public void RpcChangeBool()
    {
        networkPlayer.IsShopTime = !networkPlayer.IsShopTime;
    }

    [Command]
    private void CmdStartCoroutine()
    {
        StartCoroutine(ShowShop());
    }
    private IEnumerator ShowShop()
    {
        yield return new WaitForSecondsRealtime(5);
        
        Room.StartGame();
    }
}
