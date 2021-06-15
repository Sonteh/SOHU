using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;
using System.Linq;

public class HostGame : MonoBehaviour
{
    public void HostLobby()
    {
        NetworkManager.singleton.StartHost();
        IPAddress[] localIPsFromDns = Dns.GetHostAddresses("");
        IPAddress[] localIPsV4;

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            localIPsV4 = localIPsFromDns.Reverse().Take(2).ToArray();
            APIHelper.RegisterServer(FindIp(localIPsV4), PlayerPrefs.GetString("PlayerName"), "Arena01");
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            localIPsV4 = localIPsFromDns.Take(2).ToArray();
            APIHelper.RegisterServer(FindIp(localIPsV4), PlayerPrefs.GetString("PlayerName"), "Arena01");

        }
    }


    private string FindIp(IPAddress[] ips)
    {
        string ip;

        if (ips[1].ToString().Contains("25."))
        {
            ip = ips[1].ToString();
        }
        else
        {
            ip = ips[0].ToString();
        }

        return ip;
    }
}
