using UnityEngine;
using Mirror;

public class Fireball : NetworkBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float speed;
    [SerializeField] private float fireballCooldown;
    private float canUseFireball = -1.0f;
    private UIScript uiScript;

    private void Awake() 
    {
        uiScript = GameObject.FindObjectOfType<UIScript>();
    }

    public override void OnStartAuthority()
    {
        uiScript.fireballCooldownTime = fireballCooldown;
    }

    private void Update() 
    {
        if (!hasAuthority) {return;}

        if (Input.GetButtonDown("Fireball") && Time.time > canUseFireball && Chat.isChatActive == false)
        {
            uiScript.IsFireballUsed = true;
            canUseFireball = fireballCooldown + Time.time;
            Vector3 mouseDirection = GetPlayerMouseDirection();
            Vector3 pointToLookAt = GetPointToLookAt();
            player.transform.LookAt(pointToLookAt);

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

    private Vector3 GetPointToLookAt()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(cameraRay, out hit);

        return new Vector3(hit.point.x, hit.point.y, hit.point.z);
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