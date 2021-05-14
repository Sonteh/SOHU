using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class Player : NetworkBehaviour
{
     [SerializeField] private GameObject _fireballPrefab;
    //[SerializeField] private GameObject _magicMisslePrefab;
     [SerializeField] private float _fireballCooldown = 0.5f;
    private float _canUseFireball = -1.0f;
    //[SerializeField] private float _magicMissleCooldown = 1.0f;
    // private float _canUseMagicMissle = 1.0f;

    //[SerializeField] private Vector3 movement = new Vector3();

    // code is run only by the client

/*
    public static RaycastHit MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }
    */

    public RaycastHit hit;
    
    public override void OnStartLocalPlayer()
    {
        Vector3 playerPosition = transform.position;
        Camera.main.transform.localPosition = new Vector3(playerPosition.x - 10, Camera.main.transform.localPosition.y, playerPosition.z); //Wycentrowanie kamery na gracza
    }

    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetMouseButton(1))
        {
            MoveToCursor();
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > _canUseFireball)
        {
            //RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 myPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 direction = target - myPosition;
            direction.Normalize();
            CmdUseFireball(direction);
            //UseFireball();
        }

    }

     private RaycastHit MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

     private void MoveToCursor()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //bool hasHit = Physics.Raycast(ray, out hit);
        //var offset = hit.point - transform.position;

        RaycastHit hit = MousePosition();
        GetComponent<NavMeshAgent>().destination = hit.point;

        //if (hasHit)
        //{
        //    GetComponent<NavMeshAgent>().destination = hit.point;
        //}
    }

    [Command]
    private void CmdUseFireball(Vector3 direction)
    {
        RpcUseFireball(direction);
       // _canUseFireball = _fireballCooldown + Time.time;
       // var fireball = (GameObject)Instantiate(_fireballPrefab, transform.position + Vector3.forward, Quaternion.identity);
        //fireball.GetComponent<Rigidbody>().velocity = direction * 7.0f;
        
    }

    [ClientRpc]
    private void RpcUseFireball(Vector3 direction)
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //bool hasHit = Physics.Raycast(ray, out hit);
        //Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        _canUseFireball = _fireballCooldown + Time.time;
        var fireball = (GameObject)Instantiate(_fireballPrefab, transform.position + Vector3.forward, Quaternion.identity);

        //RaycastHit hit = MousePosition();
        //Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //Vector3 myPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //fireball.GetComponent<Transform>().Translate(target * Time.deltaTime * 7.0f);
        //Vector3 direction = target - myPosition;
        //direction.Normalize();
        fireball.GetComponent<Rigidbody>().velocity = direction * 7.0f;
        
        //Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
       // var fireballTransform = fireball.transform.position;
        //fireballTransform = Vector3.MoveTowards(fireballTransform, target, 7.0f * Time.deltaTime);
        //GetComponent<Rigidbody>().velocity = transform.forward * 15.0f; 
        //MoveToPosition(fireball);
        //fireball.GetComponent<Transform>().position = Vector3.MoveTowards(transform.position, target, 7.0f * Time.deltaTime); 
 

        /*
        if (hasHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 7.0f * Time.deltaTime);

            if (gameObject.transform.position == target)
            {
                Destroy(fireball);
            }
        }*/
        
    }

    private void MoveToPosition()
    {
        RaycastHit hit = MousePosition();
        Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //fireball.GetComponent<Transform>().position = Vector3.MoveTowards(fireball.transform.position, target, 7.0f * Time.deltaTime);
    }
}


 /*
    [ClientRpc]
    private void RpcMove() => transform.Translate(movement);

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            Damage();
            Destroy(other.gameObject);
        }
    }

    private void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void UseFireball()
    {
        _canUseFireball = _fireballCooldown + Time.time;
        Instantiate(_fireballPrefab, transform.position + Vector3.forward, Quaternion.identity);
    }

    private void UseMagicMissle()
    {
        _canUseMagicMissle = _magicMissleCooldown + Time.time;
        Instantiate(_magicMisslePrefab, transform.position + Vector3.forward, Quaternion.identity);
    }


    // Method is only called on the client
    // ulitilzes NetworkTransform - add in the Inspector
    //[Client]
    //private void Update()
    //{
    //    // check Input only for the objects that you have authority over
    //    if (!hasAuthority) { return; }


    //    if (!Input.GetKeyDown(KeyCode.Space)) { return; }

    //    transform.Translate(movement);
    //}



}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Player : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject _fireballPrefab;
//    [SerializeField]
//    private GameObject _magicMisslePrefab;
//    [SerializeField]
//    private float _fireballCooldown = 0.5f;
//    private float _canUseFireball = -1.0f;
//    [SerializeField]
//    private float _magicMissleCooldown = 1.0f;
//    private float _canUseMagicMissle = 1.0f;
//    private int _lives = 3;

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Q) && Time.time > _canUseFireball)
//        {
//            UseFireball();
//        }

//        if (Input.GetKeyDown(KeyCode.W) && Time.time > _canUseMagicMissle)
//        {
//            UseMagicMissle();
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Spell")
//        {
//            Damage();
//            Destroy(other.gameObject);
//        }
//    }

//    private void Damage()
//    {
//        _lives--;

//        if (_lives < 1)
//        {
//            Destroy(this.gameObject);
//        }
//    }

//    private void UseFireball()
//    {
//        _canUseFireball = _fireballCooldown + Time.time;
//        Instantiate(_fireballPrefab, transform.position + Vector3.forward, Quaternion.identity);
//    }

//    private void UseMagicMissle()
//    {
//        _canUseMagicMissle = _magicMissleCooldown + Time.time;
//        Instantiate(_magicMisslePrefab, transform.position + Vector3.forward, Quaternion.identity);
//    }
//}

*/