using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Net;
using System;

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
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/getAll");

        //string ip = GetIP();

        //Debug.Log("Local: " + ip);

        GetIP();

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

    public static Servers Filter(string host, string map)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/filter");

        var postData = "PlayerName=" + Uri.EscapeDataString(host);
        postData += "&map=" + Uri.EscapeDataString(map);
        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        return parseResponse(request);

    }

    public static Servers RegisterServer(string ip, string host, string map)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/add");

        Debug.Log("Inside");
        Debug.Log(ip);
        Debug.Log(host);
        Debug.Log(map);

        var postData = "IP=" + Uri.EscapeDataString(ip);
        postData += "&PlayerName=" + Uri.EscapeDataString(host);
        postData += "&map=" + Uri.EscapeDataString(map);
        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        return parseResponse(request);

    }

    private static Servers parseResponse(HttpWebRequest request)
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());

        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<Servers>(json);
    }

    private static void GetIP()
    {
        string localComputerName = Dns.GetHostName();
        Debug.Log(localComputerName);
        IPAddress[] localIPs = Dns.GetHostAddresses("");
        Debug.Log(localIPs[0]);
        Debug.Log(localIPs[1]);
    }
}