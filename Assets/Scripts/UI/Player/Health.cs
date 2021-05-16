using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour 
{
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float damage;
    [SyncVar] private float currentHealth;
    public delegate void HealthUpdateDelegate(float currentHealth, float maxHealth);
    public event HealthUpdateDelegate EventHealthUpdate;
    GameObject roundSystem;
    //[SyncVar] public int numPlayers;
    //[SyncVar(hook = nameof(HandlePlayerNumberChanged))]
    //public int numPlayers;

    //public void HandlePlayerNumberChanged(int oldValue, int newValue) => UpdatePlayers();

   // private void UpdatePlayers() 
   // {
   //     numPlayers--;
   //     Debug.Log("From UpdatePlayers() " + numPlayers);
    //}
    
    private void Start()
    {
        roundSystem = GameObject.Find("RoundSystem(Clone)");
   //     numPlayers = roundSystem.GetComponent<RoundSystem>().numberOfPlayersAlive;
   //     Debug.Log("Start(): " + numPlayers);
    }
    
    #region Server
    
    [Server]
    private void SetHealth(float value)
    {
        currentHealth = value;
        EventHealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public override void OnStartServer() => SetHealth(maxHealth);

    [Command]
    private void CmdDealDamage(float value)
    {
        SetHealth(Mathf.Max(currentHealth - value, 0));
        //int numPlayer = roundSystem.GetComponent<RoundSystem>().numberOfPlayersAlive;

        if (currentHealth == 0f)
        {
            //UpdatePlayers();
            RpcOnDeath();
            roundSystem.GetComponent<RoundSystem>().OnDeath();
            //Debug.Log("From CmdDealDamage() " + numPlayers);
        }
    }

    #endregion

    #region Client

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) {return;}
       // GameObject roundSystem = GameObject.Find("RoundSystem(Clone)");
        //int numPlayer = roundSystem.GetComponent<RoundSystem>().numberOfPlayersAlive;

       // if (currentHealth == 0)
        //{
            //--numPlayer;
            
       // } 
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        gameObject.SetActive(false);
       // Debug.Log("Number of players from Health: " + numPlayers);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if(!hasAuthority) {return;}

        if (collider.tag == "Spell")
        {   
            damage = collider.gameObject.GetComponent<SpellData>().spellDamage;
            CmdDealDamage(damage);
        }
    }

    #endregion
}
