using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HostGame : MonoBehaviour
{
    public void HostLobby()
    {
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
        NetworkManager.singleton.StartHost();
    }
}
