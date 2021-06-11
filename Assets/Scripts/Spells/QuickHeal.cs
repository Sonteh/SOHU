using UnityEngine;
using Mirror;

public class QuickHeal : NetworkBehaviour
{
    [SerializeField] private float healAmount;

    [SerializeField] private float quickHealCooldown = 2.0f;
    [SerializeField] GameObject quickHealPrefab;

    private float canUseQuickHeal = -1.0f;
    private Vector3 _mouse;
    private UIScript uiScript;
    private Health health;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    // public override void OnStartAuthority()
    // {
    //     uiScript.portableZoneCooldownTime = quickHealCooldown;
    // }

    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetKeyDown("h"))// && Time.time > canUseQuickHeal && Chat.isChatActive == false
        {
            //health.currentHealth = health.currentHealth + 20;
            canUseQuickHeal =  quickHealCooldown + Time.time;
            
            CmdUsePortableZone();
        }
    }

    [Command]
    private void CmdUsePortableZone()
    {
        RpcUsePortableZone();
    }

    [ClientRpc]
    private void RpcUsePortableZone()
    {
        var quickHeal = (GameObject)Instantiate(quickHealPrefab, transform.position, Quaternion.identity);
        //GameObject.FindObjectOfType<Health>().CmdHealDamage(20.0f);
    }
}
