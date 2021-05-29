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

    // public float liveTime = 5f;
    // private void Start()
    // {
    //     Destroy(this.gameObject, 5);
    // }

    void Update()
    {
        if (!isLocalPlayer) {return;}
        
        if (Input.GetKeyDown("r") && Time.time > canUseTacticalRecall)
        {
            //Vector3 mousePosition = GetPlayerMouseDirection();
            canUseTacticalRecall =  tacticalRecallCooldown + Time.time;
            CmdUseTacticalRecall();
        }

    //     liveTime -= Time.deltaTime;
    //     Debug.Log(liveTime);
    //  if (liveTime <= 0)
    //  {
    //     Destroy(this.gameObject);
    //  }
    }

    // private void OnTriggerEnter(Collider collider) 
    // {
    //     if (collider.tag == "Spell")
    //     {
    //         Destroy(collider.gameObject);
    //     }
    // }

    // private Vector3 GetPlayerMousePosition()
    // {
    //     RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     Physics.Raycast(ray, out hit);
    //     Vector3 mousePosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

    //     return mousePosition;
    // }

    // private Vector3 GetPlayerMouseDirection()
    // {   
    //     Vector3 mousePosition = GetPlayerMousePosition();
    //     Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //     Vector3 mouseDirection = mousePosition - playerPosition;
    //     mouseDirection.Normalize();

    //     return mouseDirection;
    // }Vector3 mousePosition mousePosition Vector3 mousePosition

    [Command]
    private void CmdUseTacticalRecall()
    {
        RpcUseTacticalRecall();
    }

    [ClientRpc]
    private void RpcUseTacticalRecall()
    {
        var tacticalRecall = (GameObject)Instantiate(tacticalRecallPrefab, transform.position, Quaternion.identity);
        //magicMissle.GetComponent<Rigidbody>().velocity = mousePosition * 7.0f;
        transform.position = new Vector3(0,0,0);
        tacticalRecall = (GameObject)Instantiate(tacticalRecallPrefab, transform.position, Quaternion.identity);
        //tacticalRecall.transform.position = transform.position; //new Vector3(transform.position, transform.position, transform.position);
    }
}
