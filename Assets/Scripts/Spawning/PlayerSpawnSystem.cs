using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;
    GameObject playerInstance;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }
    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => Network.OnServerReadied += SpawnPlayer;

    [ServerCallback]
    private void OnDestroy() => Network.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        if (spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for player {nextIndex}");
            return;
        }

        //GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        //DontDestroyOnLoad(playerInstance);
        NetworkServer.Spawn(playerInstance, conn);
        //NetworkServer.AddPlayerForConnection(conn, playerInstance);

        nextIndex++;
    }

/*
    [Server]
    public void ResetPlayerPosition(NetworkConnection conn)
    {
        int nextIndexHelper = 0;
        //conn.identity.transform.position = spawnPoints[nextIndexHelper].position;
        //Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndexHelper);
        //playerInstance.transform.position = spawnPoints[nextIndexHelper].position;
        //playerInstance.transform.position = spawnPoints[nextIndexHelper].position;
        GameObject testTest = GameObject.Find("Player(Clone)");
        testTest.GetComponent<RestartPlayerPosition>().ResetPosition();

        nextIndexHelper++;
    }
    */
}
