using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public class APIHelper
{
    //TODO better URL handling
    private static string GetUrl()
    {
        string baseUrl = "http://167.71.32.118:3000/servers";
        return baseUrl;
    }

    public static Servers GetServers()
    {
        Debug.Log(GetUrl() + "/getAll");

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/getAll");

        return parseResponse(request);

    }

    public static Servers GetSortedByArena(string dir)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/getAllSortedByArena/" + dir);

        return parseResponse(request);

    }

    public static Servers GetSortedByPlayers(string dir)
    {

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/getAllSortedByPlayers/" + dir);

        return parseResponse(request);

    }

    private static Servers parseResponse(HttpWebRequest request)
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());

        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<Servers>(json);
    }
}