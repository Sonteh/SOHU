using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] private GameObject lobby = null;


    // can be changed to OnEnable, On Disable
    private void Start()
    {
        Network.ClientOnConnected += HandleClientConnected;
    }

    private void OnDestroy()
    {
        Network.ClientOnConnected -= HandleClientConnected;
    }

    private void HandleClientConnected()
    {
        lobby.SetActive(true);
    }

    public void Leave()
    {
        // if you're a host or if you're a client
        if(NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        } else
        {
            NetworkManager.singleton.StopClient();

            SceneManager.LoadScene(0);
        }
    }
}
