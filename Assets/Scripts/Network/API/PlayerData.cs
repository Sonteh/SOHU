using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int ID;
    public string IP;
    public string PlayerName;
}

[System.Serializable]
public class Players
{
    public List<PlayerData> data;
}