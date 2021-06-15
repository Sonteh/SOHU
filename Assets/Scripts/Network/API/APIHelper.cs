using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;

public class APIHelper
{
    private static string GetUrl()
    {
        string baseUrl = "http://167.71.32.118:3000/servers";
        return baseUrl;
    }

    public static Servers GetServers()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/getAll");

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

    public static Servers UpdateServer(string map, string host)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/update");

        var postData = "map=" + Uri.EscapeDataString(map);
        postData += "&PlayerName=" + Uri.EscapeDataString(host);
        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "PUT";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        return parseResponse(request);

    }

    public static Servers DeleteServer(string host)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetUrl() + "/delete/" + host);

        request.Method = "DELETE";

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
        IPAddress[] localIPs = Dns.GetHostAddresses("");
    }
}