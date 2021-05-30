using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NetworkRoom : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[8];
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private Button changeArenaButton = null;
    [SerializeField] private Button changeAmountButtonUp = null;
    [SerializeField] private Button changeAmountButtonDown = null;
    [SerializeField] private TMP_Text amountOfRoundsDisplay = null;
    [SerializeField] public TMP_Text arenaTitle = null;
    [SerializeField] private Image arena01Image = null;
    [SerializeField] private Image arena02Image = null;
    [SerializeField] private Image confirmButton = null;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;
    [SyncVar(hook = nameof(HandleArenaChanged))]
    public bool Arena = true;
    [SyncVar(hook = nameof(HandleRoundAmountChanged))]
    public int RoundAmount = 1;

    private bool isLeader;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startGameButton.gameObject.SetActive(value);
            confirmButton.gameObject.SetActive(value);
            changeAmountButtonUp.gameObject.SetActive(value);
            changeAmountButtonDown.gameObject.SetActive(value);
            changeArenaButton.gameObject.SetActive(value);
        }
    }

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
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
        //CmdSetDisplayName(PlayerName.displayName);
        CmdSetDisplayName(PlayerPrefs.GetString("PlayerName"));

        lobbyUI.SetActive(true);
    }

    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);

        // Update UI
        UpdateDisplay();
    }

    public override void OnStopClient()
    {
        Room.RoomPlayers.Remove(this);

        UpdateDisplay();
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();
    public void HandleArenaChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleRoundAmountChanged(int oldValue, int newValue) => UpdateDisplay();

    private void UpdateDisplay()
    {
        // if a player doesn't have authority, find a player that does and update their display
        if (!hasAuthority)
        {
            foreach (var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "WAITING...";
            //playerReadyTexts[i].text = string.Empty;
        }

        for (int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            arenaTitle.text = Room.RoomPlayers[0].Arena ? "Arena02" : "Arena01";
            Debug.Log("ARENA TITLE");
            Debug.Log(arenaTitle.text);

            if (Room.RoomPlayers[0].Arena == false)
            {
                arena01Image.gameObject.SetActive(true);
                arena02Image.gameObject.SetActive(false);
            }
            else
            {
                arena01Image.gameObject.SetActive(false);
                arena02Image.gameObject.SetActive(true);
            }

            if (Room.RoomPlayers[0].RoundAmount <= 0)
            {
                amountOfRoundsDisplay.text = "1";
            }
            else
            {
                amountOfRoundsDisplay.text = Room.RoomPlayers[0].RoundAmount.ToString();
            }

            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerNameTexts[i].color = Room.RoomPlayers[i].IsReady ?
            playerNameTexts[i].color = Color.green :
            playerNameTexts[i].color = Color.red;
        }
    }

    public void HandleReadyToStart(bool readyToStart)
    {
        if (isLeader) { return; }

        startGameButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;

        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }

        roundSystem.scoreToWin = int.Parse(amountOfRoundsDisplay.text);
        room.arenaName = arenaTitle.text;

        Room.StartGame();

    }

    [Command]
    public void CmdChangeArena()
    {
        Arena = !Arena;
    }

    [Command]
    public void CmdChangeAmountUp()
    {
        RoundAmount++;
    }

    [Command]
    public void CmdChangeAmountDown()
    {
        RoundAmount--;
    }

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();

            SceneManager.LoadScene(0);
        }
    }
}
