using UnityEngine;
using Mirror;

public class QuickHeal : NetworkBehaviour
{
    [SerializeField] private float quickHealCooldown = 2.0f;
    [SerializeField] GameObject quickHealPrefab;

    private float canUseQuickHeal = -1.0f;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }
    
    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetKeyDown("h") && Time.time > canUseQuickHeal && Chat.isChatActive == false)// 
        {
            canUseQuickHeal =  quickHealCooldown + Time.time;
            
            CmdUseQuickHeal();
        }
    }

    [Command]
    private void CmdUseQuickHeal()
    {
        RpcUserQuickHeal();
    }

    [ClientRpc]
    private void RpcUserQuickHeal()
    {
        var quickHeal = (GameObject)Instantiate(quickHealPrefab, transform.position, Quaternion.identity);
    }
}
