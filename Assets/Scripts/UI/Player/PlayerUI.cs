using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using TMPro;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private TMP_Text playerGold;
    [SerializeField] private Health health;
    [SerializeField] public Image healthBarImage;
    [SerializeField] private Fireball fireball;
    [SerializeField] private Image fireballImage;
    private float fireballCooldown = 1.0f;
    private bool coolingDown = false;
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

    public void HandlePlayerGoldChanged(int oldPlayerGold, int newPlayerGold) => UpdatePlayerInfo();

    public override void OnStartAuthority()
    {
        //playerUI.SetActive(true);
    }

    private void UpdatePlayerInfo()
    {
        foreach (var player in Room.GamePlayers)
        {
            if (player.isLocalPlayer)
            {
                //Debug.Log("PLAYER GOLD: " + player.playerGold);
                playerGold.SetText(player.playerGold.ToString());
            }
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdatePlayerInfo();
        }

        if ((Input.GetButtonDown("Fireball")) && fireballImage.fillAmount == 1.0f )
        {
            fireballImage.fillAmount = 0f;
            coolingDown = true;
        }
        
        if (coolingDown)
        {
            fireballImage.fillAmount += 1.0f / fireballCooldown * Time.deltaTime;

            if (fireballImage.fillAmount >= 1.0f)
            {
                coolingDown = false;
            }
        }
    }
}