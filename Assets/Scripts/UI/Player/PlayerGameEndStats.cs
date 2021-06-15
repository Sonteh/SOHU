using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerGameEndStats : NetworkBehaviour
{
    [SerializeField] private GameObject endGameStats;
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private TMP_Text playerWon;
    [SyncVar(hook = nameof(HandlePlayerNameWon))]
    public string playerNameWon = "Winner";
    //public bool IsGameFinished;

    public void HandlePlayerNameWon(string _old, string _new)
    {
        playerWon.SetText(_new.ToString());
    }

    private void Update() 
    {
        if (!hasAuthority) { return; }
        
        if (networkPlayer.IsGameFinished) 
        {
            Debug.Log("TEST GAMEEND");
            playerWon.SetText(playerNameWon);
            endGameStats.SetActive(true);
        }
    }
}
