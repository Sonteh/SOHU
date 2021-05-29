using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PortableZone : NetworkBehaviour
{
    [SerializeField] private GameObject portableZonePrefab;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float portableZoneCooldown = 2.0f;
    private float canUsePortableZone = -1.0f;

    private Vector3 _mouse;

    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetKeyDown("2") && Time.time > canUsePortableZone)
        {
            canUsePortableZone =  portableZoneCooldown + Time.time;
            Vector3 mousePosition = GetPlayerMousePosition();
            CmdUsePortableZone(mousePosition);
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
    private void CmdUsePortableZone(Vector3 mousePosition)
    {
        RpcUsePortableZone(mousePosition);
    }

    [ClientRpc]
    private void RpcUsePortableZone(Vector3 mousePosition)
    {
        var portableZone = (GameObject)Instantiate(portableZonePrefab, mousePosition, Quaternion.identity);
    }
}
