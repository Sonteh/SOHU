using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinFromList : MonoBehaviour
{
    [SerializeField] private Text ipAddressText;
    [SerializeField] private Button joinButton;

    public void JoinLobby()
    {
        Debug.Log("IP ADDRESS");
        Debug.Log(ipAddressText);

        string ipAddress = ipAddressText.text;

        NetworkManager.singleton.networkAddress = ipAddress;
        NetworkManager.singleton.StartClient();

    }
}
