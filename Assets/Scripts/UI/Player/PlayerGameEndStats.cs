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
    [SerializeField] public TMP_Text timeText;
    private float timeRemaining = 11;

    public void HandlePlayerNameWon(string _old, string _new)
    {
        playerWon.SetText(_new.ToString());
    }

    private void Update() 
    {
        if (!hasAuthority) { return; }
        
        if (networkPlayer.IsGameFinished) 
        {
            playerWon.SetText(playerNameWon);
            endGameStats.SetActive(true);

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                DisplayTime(timeRemaining);
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.SetText(seconds.ToString());
    }
}
