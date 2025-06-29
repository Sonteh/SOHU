﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;
using System.Linq;
using System.Collections.Generic;

public class Network : NetworkManager
{
    [Header("Custom Properties")]
    [SerializeField] private int minPlayers = 2;

    [Header("Menu Scene")]
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoom roomPlayerPrefab = null;

    [Header("Game")]
    [SerializeField] private NetworkPlayer gamePlayerPrefab = null;
    [SerializeField] private GameObject playerSpawnSystem = null;

    // Network Events
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;

    // List of Players in the Lobby
    public List<NetworkRoom> RoomPlayers { get; } = new List<NetworkRoom>();

    // List of Players in the Game
    public List<NetworkPlayer> GamePlayers { get; } = new List<NetworkPlayer>();



    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            // TODO Load Start Menu Canvas
            conn.Disconnect();
            return;
        }

        // Stops players from joining if the game is currently in progress
        if (SceneManager.GetActiveScene().path != menuScene)
        {
            // TODO Load Start Menu Canvas
            conn.Disconnect();
            return;
        }

    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // if you're in the menu scene
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            // create a room
            NetworkRoom roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader;

            // tie the connection to the game player object / prefab
            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoom>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }

    }


    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }

    public void StartGame()
    {

        if (SceneManager.GetActiveScene().path == menuScene)
        {
            //if (!IsReadyToStart()) { return; }
        }

        //logic to implemenet map choosing
        ServerChangeScene("Arena01");
    }

    public override void ServerChangeScene(string newSceneName)
    {

        if (SceneManager.GetActiveScene().name == menuScene && newSceneName.StartsWith("Arena"))
        {
            for (int i = RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                var gameplayerInstance = Instantiate(gamePlayerPrefab);
                gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);

                NetworkServer.Destroy(conn.identity.gameObject);


            }
        }

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith("Arena"))
        {
            GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);
        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }

}
