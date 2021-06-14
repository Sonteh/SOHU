using UnityEngine;
using Mirror;

public class QuickHeal : NetworkBehaviour
{
    [SerializeField] public float quickHealCooldown;
    [SerializeField] GameObject quickHealPrefab;

    private float canUseQuickHeal = -1.0f;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    public override void OnStartAuthority()
    {
        uiScript.quickHealCooldownTime = quickHealCooldown;
    }
    
    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetButtonDown("QuickHeal") && Time.time > canUseQuickHeal && Chat.isChatActive == false) 
        {
            uiScript.IsQuickHealUsed = true;
            canUseQuickHeal = quickHealCooldown + Time.time;
            
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
