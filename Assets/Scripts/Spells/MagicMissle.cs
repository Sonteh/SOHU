using UnityEngine;
using Mirror;

public class MagicMissle : NetworkBehaviour
{
    [SerializeField] private GameObject magicMisslePrefab;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float magicMissleCooldown = 2.0f;
    private float canUseMagicMissle = -1.0f;

    void Update()
    {
        if (!hasAuthority) {return;}
        
        if (Input.GetButtonDown("MagicMissle") && Time.time > canUseMagicMissle)
        {
            canUseMagicMissle =  magicMissleCooldown + Time.time;
            Vector3 mousePosition = GetPlayerMouseDirection();
            CmdUseMagicMissle(mousePosition);
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
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 mouseDirection = mousePosition - playerPosition;
        mouseDirection.Normalize();

        return mouseDirection;
    }

    [Command]
    private void CmdUseMagicMissle(Vector3 mousePosition)
    {
        RpcUseMagicMissle(mousePosition);
    }

    [ClientRpc]
    private void RpcUseMagicMissle(Vector3 mousePosition)
    {
        var magicMissle = (GameObject)Instantiate(magicMisslePrefab, spawnLocation.transform.position, Quaternion.identity);
        magicMissle.GetComponent<Rigidbody>().velocity = mousePosition * speed;
    }
}
