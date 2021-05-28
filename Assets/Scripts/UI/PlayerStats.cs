using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] private GameObject statsUI;
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private TMP_Text[] playerName = new TMP_Text[2];
    [SerializeField] private TMP_Text[] playerScore = new TMP_Text[2]; 

    [SyncVar(hook = nameof(HandlePlayerNickChanged))]
    public string playerDisplayName = "Player";
    [SyncVar(hook = nameof(HandlePlayerScoreChanged))]
    public int playerScorePoints = 0;

    private Network room;
    private Network Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as Network;
        }
    }
    public override void OnStartAuthority()
    {
        playerDisplayName = PlayerPrefs.GetString("PlayerName");
        UpdateStatistics();
        //base.OnStartAuthority();
    }

    private void Update()
    {
        
        if (!hasAuthority) {return;}
        

        if(Input.GetButton("Statistics"))
        {
            statsUI.SetActive(true);
        }
        else
        {
            statsUI.SetActive(false);
        }
    }

    public void HandlePlayerNickChanged(string oldPlayerNick, string newPlayerNick) => UpdateStatistics();
    public void HandlePlayerScoreChanged(int oldPlayerScore, int newPlayerScore) => UpdateStatistics();

    private void UpdateStatistics()
    {
        /*
        if(!hasAuthority)
        {
            foreach (var player in Room.GamePlayers)
            {
                if (player.hasAuthority)
                {
                    UpdateStatistics();
                    break;
                }
            }
            return;
        }*/

        for (int i = 0; i < Room.GamePlayers.Count; i++)
        {
            playerName[i].text = Room.GamePlayers[i].displayName;
            //playerScore[i].text = Room.GamePlayers[i].playerScore.ToString();
            playerScore[i].SetText(Room.GamePlayers[i].playerScore.ToString());
        }
    }
}
