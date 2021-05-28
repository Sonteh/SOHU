using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServersList : MonoBehaviour
{
    public void Refresh()
    {
        Debug.Log("here");

        var pd = APIHelper.GetPlayers();

        Debug.Log(pd.data[0].IP);
    }
}
