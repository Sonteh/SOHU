using UnityEngine;
using Mirror;

public class MagicMissle : NetworkBehaviour
{
    [SerializeField] private GameObject magicMisslePrefab;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float magicMissleCooldown = 2.0f;
    private float canUseMagicMissle = -1.0f;

    void Update()
    {
        if (!isLocalPlayer) {return;}
        
        if (Input.GetButtonDown("MagicMissle"))
        {
            Vector3 mousePosition = GetPlayerMouseDirection();
            canUseMagicMissle =  magicMissleCooldown + Time.time;
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
        var magicMissle = (GameObject)Instantiate(magicMisslePrefab, transform.position + Vector3.forward, Quaternion.identity);
        magicMissle.GetComponent<Rigidbody>().velocity = mousePosition * 7.0f;
        //magicMissle.transform.position = Vector3.MoveTowards(magicMissle.transform.position, mousePosition, 7.0f * Time.deltaTime);
    }
}
