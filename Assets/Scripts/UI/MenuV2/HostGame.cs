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

/* TODO
    public void Refresh()
    {
        Debug.Log("here");

        var pd = APIHelper.GetPlayers();

        Debug.Log(pd.data[0].IP);
    }
    */
}
