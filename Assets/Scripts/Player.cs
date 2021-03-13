using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private GameObject _fireballPrefab;
    [SerializeField]
    private GameObject _magicMisslePrefab;
    [SerializeField]
    private float _fireballCooldown = 0.5f;
    private float _canUseFireball = -1.0f;
    [SerializeField]
    private float _magicMissleCooldown = 1.0f;
    private float _canUseMagicMissle = 1.0f;
    private int _lives = 3;

    // field is visible in the unity inspector
    [SerializeField] private Vector3 movement = new Vector3();

    // code is run only by the client
    [Client]
    private void Update()
    {
        // check Input only for the objects that you have authority over
        if (!hasAuthority) { return; }

        // move on pressing space
        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        // Call the logic to be done by the server
        CmdMove();

    }

    // Logic done on the server
    [Command]
    private void CmdMove()
    {
        RpcMove();

        //if (Input.GetKeyDown(KeyCode.Q) && Time.time > _canUseFireball)
        //{
        //    UseFireball();
        //}

        //if (Input.GetKeyDown(KeyCode.W) && Time.time > _canUseMagicMissle)
        //{
        //    UseMagicMissle();
        //}
    }

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