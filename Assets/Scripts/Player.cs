using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject myMaterial;
    [SerializeField] NetworkPlayer networkPlayer;
    [SerializeField] TextMesh playerNameText;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;
    private Material playerMaterialClone;

    private void OnNameChanged(string _Old, string _New) 
    {
        playerNameText.text = playerName;
    }

    private void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
        playerMaterialClone = myMaterial.GetComponent<Renderer>().material;
        playerMaterialClone.color = _New;
        myMaterial.GetComponent<Renderer>().material = playerMaterialClone;
    }

    public override void OnStartAuthority()
    {
        Vector3 playerPosition = transform.position;
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 10, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza

        string name = PlayerPrefs.GetString("PlayerName");
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
 
        CmdSetPlayerInfo(name, color);
    }

    [Command]
    private void CmdSetPlayerInfo(string _name, Color color)
    {
        playerName = _name;
        playerColor = color;
    }
}