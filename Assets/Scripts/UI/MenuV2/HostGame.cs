using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;

public class HostGame : MonoBehaviour
{
    public void HostLobby()
    {
        //Debug.Log(PlayerPrefs.GetString("PlayerName"));
        NetworkManager.singleton.StartHost();

        //string player = filterPlayersInput.text;
        //string arena = filterArenasInput.text;
        IPAddress[] localIPs = Dns.GetHostAddresses("");
        //Debug.Log(localIPs[0]);
        APIHelper.RegisterServer(localIPs[0].ToString(), PlayerPrefs.GetString("PlayerName"), "arena01");

    }
}
