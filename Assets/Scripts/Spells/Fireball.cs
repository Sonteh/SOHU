using UnityEngine;
using Mirror;

public class Fireball : NetworkBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private float fireballCooldown = 2.0f;
    private float canUseFireball = -1.0f;

    private void Update() 
    {
        if (!hasAuthority) {return;}
 
        if (Input.GetButtonDown("Fireball") && Time.time > canUseFireball)
        {
            canUseFireball = fireballCooldown + Time.time;
            Vector3 mouseDirection = GetPlayerMouseDirection();
            CmdUseFireball(mouseDirection);
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
    
    [Command]
    void Destroy()
    {
            Destroy(gameObject);
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
    private void CmdUseFireball(Vector3 mouseDirection)
    {
        RpcUseFireball(mouseDirection);
    }

    [ClientRpc]
    private void RpcUseFireball(Vector3 mouseDirection)
    {
        var fireball = (GameObject)Instantiate(fireballPrefab, spawnLocation.transform.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().velocity = mouseDirection * speed;
    }
}