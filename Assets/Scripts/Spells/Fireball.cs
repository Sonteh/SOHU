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
    //public float testowyFireball = 0f;

    private void Start()
    {
        //playerUI.fireballCooldown = 0f;
        //Debug.Log("Fireball cooldown from Fireball: " + playerUI.fireballCooldown);
        //playerUI.fireballImage.fillAmount = 1.0f;
        //playerUI.fireballImage.fillAmount = 1.0f;
        // Debug.Log("testowy fill: " + playerUI.fireballImage.fillAmount);
    }
    
    private void Update() 
    {
        if (!hasAuthority) {return;}
        //playerUI.coolingDown = false;
        //playerUI.fireballImage.fillAmount = 0f;
        //Debug.Log(playerUI.fireballImage.fillAmount);
        if (Input.GetButtonDown("Fireball") && Time.time > canUseFireball)
        {
            canUseFireball = fireballCooldown + Time.time;
            
            //playerUI.fireballImage.fillAmount = 0f;
            //TargetFillAmount();
            //TargetTestowyCooldown(canUseFireball);
            Vector3 mouseDirection = GetPlayerMouseDirection();
            CmdUseFireball(mouseDirection);
        }
    }

    // [TargetRpc]
    // private void TargetTestowyCooldown(float fireballCooldown)
    // {
    //     //playerUI
    //    // playerUI.UpdateFireball(fireballCooldown);
    //     playerUI.coolingDown = true;
    //     Debug.Log("Cooling down = " + playerUI.coolingDown);
    //     Debug.Log("Fireball cooldown from Fireball: " + Time.time + " ------------ " + fireballCooldown);
    // }

    // [TargetRpc]
    // private void TargetFillAmount()
    // {
    //     //playerUI.fireballImage.fillAmount = 0f;
    //     playerUI.UpdateFireball(0f);
        
    //     //Debug.Log("fill = " + playerUI.fireballImage.fillAmount);
    // }

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