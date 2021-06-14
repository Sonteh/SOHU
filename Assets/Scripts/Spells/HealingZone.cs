using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealingZone : NetworkBehaviour
{
    [SerializeField] private GameObject healingZonePrefab;
    [SerializeField] public float healingZoneCooldown;
    private float canUseHealingZone = -1.0f;
    private Vector3 _mouse;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    public override void OnStartAuthority()
    {
        uiScript.healingZoneCooldownTime = healingZoneCooldown;
    }

    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetButtonDown("HealingZone") && Time.time > canUseHealingZone && Chat.isChatActive == false)
        {
            uiScript.IsHealingZoneUsed = true;
            canUseHealingZone =  healingZoneCooldown + Time.time;
            Vector3 mousePosition = GetPlayerMousePosition();
            
            CmdUseHealingZone(mousePosition);
        }
    }

    private Vector3 GetPlayerMousePosition()
    {
        _mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(_mouse);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        return target;
    }

    [Command]
    private void CmdUseHealingZone(Vector3 mousePosition)
    {
        RpcUseHealingZone(mousePosition);
    }

    [ClientRpc]
    private void RpcUseHealingZone(Vector3 mousePosition)
    {
        var portableZone = (GameObject)Instantiate(healingZonePrefab, mousePosition, Quaternion.identity);
    }
}
