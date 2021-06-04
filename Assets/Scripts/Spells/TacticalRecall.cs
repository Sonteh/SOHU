using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TacticalRecall : NetworkBehaviour
{
    [SerializeField] private GameObject tacticalRecallPrefab;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float tacticalRecallCooldown = 2.0f;
    private float canUseTacticalRecall = -1.0f;

    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetKeyDown("r") && Time.time > canUseTacticalRecall && Chat.isChatActive == false)
        {
            canUseTacticalRecall =  tacticalRecallCooldown + Time.time;
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
