using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class TacticalRecall : NetworkBehaviour
{
    [SerializeField] private GameObject tacticalRecallPrefab;
    [SerializeField] public float tacticalRecallCooldown = 2.0f;
    private float canUseTacticalRecall = -1.0f;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    public override void OnStartAuthority()
    {
        uiScript.tacticalRecallCooldownTime = tacticalRecallCooldown;
    }

    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetButtonDown("TacticalRecall") && Time.time > canUseTacticalRecall && Chat.isChatActive == false)
        {
            uiScript.IsTacticalRecallUsed = true;
            canUseTacticalRecall =  tacticalRecallCooldown + Time.time;
            GetComponent<NavMeshAgent>().destination = new Vector3(0,0,0);
            
            CmdUseTacticalRecall();
        }
    }

    [Command]
    private void CmdUseTacticalRecall()
    {
        RpcUseTacticalRecall();
    }

    [ClientRpc]
    private void RpcUseTacticalRecall()
    {
        var tacticalRecall = (GameObject)Instantiate(tacticalRecallPrefab, transform.position, Quaternion.identity);
        transform.position = new Vector3(0,0,0);
        
        tacticalRecall = (GameObject)Instantiate(tacticalRecallPrefab, transform.position, Quaternion.identity);
    }
}
