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
        string localIp;
        Debug.Log(localIPs[0]);
        Debug.Log(localIPs[1]);
        Debug.Log(localIPs[2]);
        Debug.Log(localIPs[3]);
        Debug.Log(localIPs[4]);
        Debug.Log(localIPs[5]);
        Debug.Log(localIPs[6]);
        Debug.Log(localIPs[7]);
        Debug.Log(localIPs[8]);
        Debug.Log(localIPs[9]);

        Debug.Log("len: " + localIPs.Length);
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {

        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            //IPAddress[] localIpsList = [local]


            if (localIPs.Length == 2)
            {
                localIp = localIPs[1].ToString();
                Debug.Log("LocalIp IF: " + localIp);
            }
            else
            {
                localIp = localIPs[0].ToString();
                Debug.Log("LocalIp: Else" + localIp);
            }

            Debug.Log("WHAT IS MY IP AFTER ALL THIS SHIT: " + localIp);
            APIHelper.RegisterServer(localIp, PlayerPrefs.GetString("PlayerName"), "Arena01");
        }

    }
}
