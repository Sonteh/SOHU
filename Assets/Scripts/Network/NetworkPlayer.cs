using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private Player playerScript;
    [SerializeField] private Health health;
    [SyncVar]
    public string displayName = "Loading...";
    [SyncVar]
    public int playerScore = 0;
    [SyncVar]
    public bool IsShopTime = false;
    public bool IsSpellBought = false;
    //public RollingMeteor rollingMeteor;

    private Network room;
    private Network Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as Network;
        }
    }
    private void Start()
    {
        if (!isLocalPlayer) {return;}

        //rollingMeteor = player.GetComponent<RollingMeteor>();
    }

    private void Update() 
    {
        if (!isLocalPlayer) {return;}

        Debug.Log("Update w NetworkPlayer : " + IsSpellBought);
        
        if (IsSpellBought)
        {
           // rollingMeteor.enabled = true;
        }
    }

    public override void OnStartClient()
    {
        Debug.Log("Game Player Add from OnStartClient");
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    public override void OnStartAuthority()
    {
        SetDisplayName(PlayerPrefs.GetString("PlayerName"));
        playerScript.testowyBool = false;
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Server]
    public void IncrementPlayerScore()
    {
        playerScore++;
    }

    [Server]
    public void PlayerBoughtSpell()
    {

        Debug.Log("IsSpellBought from Network Player before: " + IsSpellBought);
        IsSpellBought = true;
        Debug.Log("IsSpellBought from Network Player after: " + IsSpellBought);
        //playerScript.testowyBool = true;
        // Debug.Log("testoswyBool from Network Player before: " + playerScript.testowyBool);
        // playerScript.testowyBool = true;
        // Debug.Log("testoswyBool from Network Player after: " + playerScript.testowyBool);
        //CmdUpdateBool();
        TargetTest();
    }

    //If the first parameter of your TargetRpc method is a NetworkConnection then that's the connection that will receive the message regardless of context.
    //If the first parameter is any other type, then the owner client of the object with the script containing your TargetRpc will receive the message.
    [TargetRpc]
    public void TargetTest()
    {
        playerScript.testowyBool = true;
    }

    // [Command]
    // private void CmdUpdateBool()
    // {
    //     playerScript.testowyBool = true;
    // }
}