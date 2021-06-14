using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RollingMeteor : NetworkBehaviour
{
    [SerializeField] private GameObject rollingMeteorPrefab;
    [SerializeField] public float speed = 7.0f;
    [SerializeField] public float rollingMeteorCooldown = 0.5f;
    [SerializeField] private GameObject spawnLocation;
    private float canUseRollingMeteor = -1.0f;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    public override void OnStartAuthority()
    {
        uiScript.rollingMeteorCooldownTime = rollingMeteorCooldown;
    }
    
    private void Update() 
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetButtonDown("RollingMeteor") && Time.time > canUseRollingMeteor && Chat.isChatActive == false)
        {
            uiScript.IsRollingMeteorUsed = true;
            canUseRollingMeteor = rollingMeteorCooldown + Time.time;
            Vector3 mouseDirection = GetPlayerMouseDirection();
            
            CmdUseRollingMeteor(mouseDirection);
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Spell")
        {
            Destroy(collider.gameObject);
        }
    }

    private Vector3 GetPlayerMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        Vector3 mousePosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        return mousePosition;
    }

    private Vector3 GetPlayerMouseDirection()
    {   
        Vector3 mousePosition = GetPlayerMousePosition();
        Vector3 playerPosition = new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y, spawnLocation.transform.position.z);
        Vector3 mouseDirection = mousePosition - playerPosition;
        mouseDirection.Normalize();

        return mouseDirection;
    }

    [Command]
    private void CmdUseRollingMeteor(Vector3 mouseDirection)
    {
        RpcUseRollingMeteor(mouseDirection);
    }

    [ClientRpc]
    private void RpcUseRollingMeteor(Vector3 mouseDirection)
    {
        var rollingMeteor = (GameObject)Instantiate(rollingMeteorPrefab, spawnLocation.transform.position, Quaternion.identity);
        rollingMeteor.GetComponent<Rigidbody>().velocity = mouseDirection * speed;
    }
}