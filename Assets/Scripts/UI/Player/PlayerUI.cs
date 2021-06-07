using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] public Image healthBarImage;
    [SerializeField] private Fireball fireball;
    [SerializeField] private Image fireballImage;
    private float fireballCooldown = 1.0f;
    private bool coolingDown = false;
    private float maxHealth;
    public float currentHealth;
    private float currentHealthChanged;

    private Network room;
    private Network Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as Network;
        }
    }

    // public void SetPlayerHealth(float value)
    // {
    //     Debug.Log("SetPlayerHealth: " + value);
    //     currentHealth = value;
    //     //healthBarImage.fillAmount = currentHealth / maxHealth;
    //     //UpdateHealth(currentHealth);
    // }

    // private void UpdateHealth(float value)
    // {
    //     Debug.Log("Current health work: " + value + " max health is " + maxHealth + " fillAmount " + healthBarImage.fillAmount);
    //     healthBarImage.fillAmount = value / 100;
    //     Debug.Log("Only fillAmount " + healthBarImage.fillAmount);
    // }

    public override void OnStartAuthority()
    {
        maxHealth = health.maxHealth;
        currentHealth = maxHealth;
        healthBarImage.fillAmount = health.maxHealth / 100;
    }

    [Server]
    public void UpdateHealth(NetworkConnection conn, float value)
    {
       // Debug.Log("VALUE IS: " + value + " CURRENT HEALTH IS: " + currentHealth);
        currentHealth = value;
        //Debug.Log("CURRENT HEALTH IS: " + currentHealth);
        //UpdateBar(currentHealth);
        foreach (var player in Room.GamePlayers)
        {
            if (player.connectionToClient == conn)
            {
                //Debug.Log("HealthBarFILL: " + healthBarImage.fillAmount);
                healthBarImage.fillAmount = currentHealth / 100;
            }
        }
    }

    private void UpdateBar(float value)
    {
        //Debug.Log("FILL AMOUNT IS: " + healthBarImage.fillAmount);
        healthBarImage.fillAmount = 10 / 100;
        //Debug.Log("FILL AMOUNT IS CHANGED: " + healthBarImage.fillAmount);
    }

    void Update()
    {
        if (!isLocalPlayer) {return;}
        //Debug.Log(currentHealth);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //maxHealth -= 10;
            //healthBarImage.fillAmount = maxHealth / 100;
        }

        if ((Input.GetButtonDown("Fireball")) && fireballImage.fillAmount == 1.0f )
        {
            fireballImage.fillAmount = 0f;
            coolingDown = true;
        }
        
        //Debug.Log("CoolingDown = " + coolingDown);
        if (coolingDown)
        {
            //Debug.Log("FILLING");
            fireballImage.fillAmount += 1.0f / fireballCooldown * Time.deltaTime;

            if (fireballImage.fillAmount >= 1.0f)
            {
                //Debug.Log("FILLED");
                coolingDown = false;
            }
        }
    }
}