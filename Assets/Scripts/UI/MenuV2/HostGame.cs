using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;

public class HostGame : MonoBehaviour
{
    public void HostLobby()
    {
        NetworkManager.singleton.StartHost();
        IPAddress[] localIPs = Dns.GetHostAddresses("");
        APIHelper.RegisterServer(localIPs[0].ToString(), PlayerPrefs.GetString("PlayerName"), "Arena01");
    }
}
