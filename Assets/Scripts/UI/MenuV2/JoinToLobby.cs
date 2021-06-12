using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinToLobby : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField ipAddressInputField;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button closeButton;

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        NetworkManager.singleton.networkAddress = ipAddress;
        NetworkManager.singleton.StartClient();
        //joinButton.interactable = false;

    }
}
