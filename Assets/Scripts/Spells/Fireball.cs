using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Fireball : NetworkBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    [SerializeField]
    private float _outOfRange = 10.0f;

    private Vector3 _mouse;
    private Ray _ray;

    private void Update() 
    {
       // if (!isLocalPlayer) {return;}
      //GetComponent<Rigidbody>().velocity = transform.forward * 15.0f; 
        
        

       // Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

      //  transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime); 
    }

/*
    //[Client]
    private void Start()
    {
        //if (!hasAuthority) { enabled = false; }
        if (!isLocalPlayer) {return;}
        _mouse = Input.mousePosition;
        _ray = Camera.main.ScreenPointToRay(_mouse);
        
    }

    [ClientRpc]
    void Update()
    {

        RaycastHit hit;
        Physics.Raycast(_ray, out hit);

        Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (gameObject.transform.position == target){
            Destroy();
        }

        Destroy(gameObject, _outOfRange);
        
        //CmdFireballNetwork();
  
    }
    
    [Command]
    void Destroy()
    {
            Destroy(gameObject);
    }
*/
    //[Command]
    //private void CmdFireballNetwork()
    //{
    //    RaycastHit hit;
    //    Physics.Raycast(_ray, out hit);

    //    Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

    //    transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

    //    Destroy(gameObject, _outOfRange);

    //    //NetworkServer.Destroy(gameObject, _out);
    //}

}


