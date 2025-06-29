﻿using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Join : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject startMenu = null;
    [SerializeField] private InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;
    [SerializeField] private Button closeButton = null;

    private void OnEnable()
    {
        Network.ClientOnConnected += HandleClientConnected;
        Network.ClientOnDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        Network.ClientOnConnected -= HandleClientConnected;
        Network.ClientOnDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        NetworkManager.singleton.networkAddress = ipAddress;
        NetworkManager.singleton.StartClient();

        joinButton.interactable = false;

    }

    public void CloseIpDialog()
    {
        Debug.Log("here");
        gameObject.SetActive(false);
        startMenu.SetActive(true);
        Debug.Log("here after");
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