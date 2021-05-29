using System.Collections.Generic;

[System.Serializable]
public class ServerData
{
    public int ID;
    public string IP;
    public string PlayerName;
    public string map;
}

[System.Serializable]
public class Servers
{
    public List<ServerData> data;
}