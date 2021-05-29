using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject myMaterial;
    [SerializeField] NetworkPlayer networkPlayer;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;
    private Material playerMaterialClone;

    private void OnColorChanged(Color _Old, Color _New)
    {
        //playerMaterialClone = GetComponent<>s
        playerMaterialClone = myMaterial.GetComponent<Renderer>().material;
        playerMaterialClone.color = _New;
        myMaterial.GetComponent<Renderer>().material = playerMaterialClone;
    }

    public override void OnStartAuthority()
    {
        Vector3 playerPosition = transform.position;
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 10, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza

        //myMaterial.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetPlayerColor(color);
    }

    [Command]
    public void CmdSetPlayerColor(Color color)
    {
        playerColor = color;
    }
}