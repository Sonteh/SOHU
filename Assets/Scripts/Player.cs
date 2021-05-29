using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] NetworkPlayer networkPlayer;
    [SerializeField] TextMesh playerNameText;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    private void OnNameChanged(string _Old, string _New) 
    {
        playerNameText.text = playerName;
    }

    public override void OnStartAuthority()
    {
        Vector3 playerPosition = transform.position;
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 10, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza

        string name = PlayerPrefs.GetString("PlayerName");
        CmdSetPlayerName(name);
    }

    [Command]
    private void CmdSetPlayerName(string _name)
    {
        playerName = _name;
    }
}