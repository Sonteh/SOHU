using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.SceneManagement;

public class RoundSystem : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private PlayerSpawnSystem playerSpawnSystem;
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
            remainingPlayers[0].IncrementPlayerScore();
            
            if (remainingPlayers[0].playerScore == scoreToWin)
            {
                foreach (var player in Room.GamePlayers)
                {
                    player.IsGameFinished = true;
                    player.SetStatsActive(remainingPlayers[0].displayName);
                }
                
                HandleGameEnd();
                return;
            }

            remainingPlayers[0].GivePlayerGold(100);

            foreach (var player in Room.GamePlayers)
            {
                player.IsShopTime = true;
            }

            HandleRoundEnd();
            
            return;
        }
    }

    [Server]
    private void HandleGameEnd()
    {
        StartCoroutine(EndGame());
    }

    [ClientRpc]
    private void RpcStartCountdown()
    {
        animator.enabled = true;
        Chat.isChatActive = true;
    }

    [ClientRpc]
    private void RpcStartRound()
    {
        Chat.isChatActive = false;
    }

    [Server]
    private void HandleRoundEnd()
    {
        StartCoroutine(ShowShop());
    }

    private IEnumerator ShowShop()
    {
        yield return new WaitForSecondsRealtime(15);
        
        Room.StartGame();
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(10);

        NetworkManager.singleton.ServerChangeScene("Menus");
    }
}
