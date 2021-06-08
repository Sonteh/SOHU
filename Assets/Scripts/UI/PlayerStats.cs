using UnityEngine;
using Mirror;
using TMPro;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] private GameObject statsUI;
    [SerializeField] private TMP_Text[] playerName = new TMP_Text[8];
    [SerializeField] private TMP_Text[] playerScore = new TMP_Text[8]; 
    [SerializeField] private TMP_Text[] playerGold = new TMP_Text[8];

    [SyncVar(hook = nameof(HandlePlayerNickChanged))]
    public string playerDisplayName = "Player";
    [SyncVar(hook = nameof(HandlePlayerScoreChanged))]
    public int playerScorePoints = 0;
    [SyncVar(hook = nameof(HandlePlayerGoldChanged))]
    public int playerGoldAmount = 0;

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
    }

    private void Update()
    {
        if (!hasAuthority) {return;}

        if(Input.GetButton("Statistics"))
        {
            UpdateStatistics();
            statsUI.SetActive(true);
        }
        else
        {
            statsUI.SetActive(false);
        }
    }

    public void HandlePlayerNickChanged(string oldPlayerNick, string newPlayerNick) => UpdateStatistics();
    public void HandlePlayerScoreChanged(int oldPlayerScore, int newPlayerScore) => UpdateStatistics();
    public void HandlePlayerGoldChanged(int oldPlayerGold, int newPlayerGold) => UpdateStatistics();

    private void UpdateStatistics()
    {
        for (int i = 0; i < Room.GamePlayers.Count; i++)
        {
            playerName[i].text = Room.GamePlayers[i].displayName;
            playerGold[i].SetText(Room.GamePlayers[i].playerGold.ToString());
            playerScore[i].SetText(Room.GamePlayers[i].playerScore.ToString());
        }
    }
}
