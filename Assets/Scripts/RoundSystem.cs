using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.UI;

public class RoundSystem : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gameMenu;

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
        gameMenu.SetActive(false);
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
        gameMenu.SetActive(true);

        RpcStartCountdown();
    }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcStartCountdown()
    {
        animator.enabled = true;
        gameMenu.SetActive(true);
    }

    [ClientRpc]
    private void RpcStartRound()
    {
        Debug.Log("Start");
    }
    
    #endregion
}
