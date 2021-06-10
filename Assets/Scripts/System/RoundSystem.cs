using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class RoundSystem : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkPlayer networkPlayer;
    public int scoreToWin;
    public List<NetworkPlayer> remainingPlayers;

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

    [Server]
    private void CheckToStartRound(NetworkConnection conn)
    {
        if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count) {return;}

        foreach (var player in Room.GamePlayers)
        {
            player.IsShopTime = false;
        }
        
        animator.enabled = true;
        remainingPlayers = Room.GamePlayers.ToList();

        RpcStartCountdown();
    }

    [Server]
    public void OnDeath(NetworkConnection connectionToClient)
    {
        foreach (var player in remainingPlayers)
        {
            if (player.connectionToClient == connectionToClient)
            {
                player.GivePlayerGold(50);
                remainingPlayers.Remove(player);

                break;
            }
        }
        
        if (remainingPlayers.Count == 1)
        {
            remainingPlayers[0].GivePlayerGold(100);
            remainingPlayers[0].IncrementPlayerScore();

            foreach (var player in Room.GamePlayers)
            {
                player.IsShopTime = true;
                
                if (player.playerScore == scoreToWin)
                {
                    HandleGameEnd();

                    return;
                }
            }
            HandleRoundEnd();
            
            return;
        }
    }

    [Server]
    private void HandleGameEnd()
    {
        //TODO: Display player nickname and scoreboard.
        Debug.Log("Player " + remainingPlayers[0].displayName + " WON!");
        Room.RoomPlayers.Clear();
        Room.GamePlayers.Clear();
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
        // Room.StopHost();
        // Room.StopClient();
        // Room.StopServer();
    }

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

    [Server]
    private void HandleRoundEnd()
    {
        StartCoroutine(ShowShop());
    }

    private IEnumerator ShowShop()
    {
        yield return new WaitForSecondsRealtime(8);
        
        Room.StartGame();
    }
}
