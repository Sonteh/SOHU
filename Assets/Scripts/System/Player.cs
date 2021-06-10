using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject myMaterial;
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private TextMesh playerNameText;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;
    private Material playerMaterialClone;
    public bool IsMagicMissleBought = false;
    public bool IsMeteorBought = false;
    public bool IsPortableZoneBought = false;
    public bool IsRecallBought = false;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

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
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 15, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza

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

    private void Start() 
    {
        if (!hasAuthority) {return;}

        if (IsMagicMissleBought)
        {
            GetComponent<MagicMissle>().enabled = true;
            uiScript.magicMissleImage.gameObject.SetActive(true);
        }

        if (IsMeteorBought)
        {
            GetComponent<RollingMeteor>().enabled = true;
            uiScript.rollingMeteorImage.gameObject.SetActive(true);
        }

        if (IsPortableZoneBought)
        {
            GetComponent<PortableZone>().enabled = true;
            uiScript.portableZoneImage.gameObject.SetActive(true);
        }

        if (IsRecallBought)
        {
            GetComponent<TacticalRecall>().enabled = true;
            uiScript.tacticalRecallImage.gameObject.SetActive(true);
        }
    }

    private void Update() {
        
        if (!hasAuthority) {return;}

        if (Input.GetButtonDown("CenterCamera"))
        {
            Vector3 playerPosition = transform.position;
            Camera.main.transform.localPosition = new Vector3(playerPosition.x - 15, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza
        }
    }
}