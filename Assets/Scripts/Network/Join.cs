using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Join : MonoBehaviour
{
    [SerializeField] private Network networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject startMenu = null;
    [SerializeField] private InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        Network.OnClientConnected += HandleClientConnected;
        Network.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        Network.OnClientConnected -= HandleClientConnected;
        Network.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = true;
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        startMenu.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}