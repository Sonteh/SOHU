using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class Mover : NetworkBehaviour
{

    private Animator animator;
    public Vector3 lastPos;
    public Vector3 curPos;

    public Rigidbody rb;
    CharacterController controller;
    NavMeshAgent test;

    private void Awake() {
        animator = GetComponentInChildren<Animator> ();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
        // = GetComponent<CharacterController>();
        test = GetComponent<NavMeshAgent>();
    }

    [ClientCallback]
    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetButton("Fire2"))
        {
            MoveToCursor();
            //animator.SetFloat("movement", 1);
        }

        // if (!hasAuthority) {return;}
        // MotionChecker();
        if(test.velocity != Vector3.zero) 
     {
         animator.SetFloat("movement", 1);
     }
     else
     {
         animator.SetFloat("movement", 0);
     }
    }

    //[Client]
    [Command]
    private void MotionChecker()
    {
        // Debug.Log("MYSZ" + GetPlayerMousePosition().point);
        // Debug.Log("GRACZ" + transform.position);
        // // lastPos = transform.position;
        // // yield return new WaitForSeconds(1);
        // if (GetPlayerMousePosition().point == transform.position)
        // {
        //     animator.SetFloat("movement", 1);
        // }
        // else
        // {
        //     animator.SetFloat("movement", 0);
        // }
        ChangeParameter();
    }

    [ClientRpc]
    private void ChangeParameter()
    {
    // curPos = transform.position;
    //  if(curPos == lastPos) 
    //  {
    //      animator.SetFloat("movement", 1);
    //  }
    //  else
    //  {
    //      animator.SetFloat("movement", 0);
    //  }
    //  lastPos = curPos;
    
    //float overallSpeed = controller.velocity.magnitude;
    // float velocity = agent.velocity.magnitude/agent.speed;
    // float speed = rb.velocity;

    //GOOOOOOOOOOOD
    Debug.Log("velocity: " + test.velocity);
    if(test.velocity != Vector3.zero) 
     {
         animator.SetFloat("movement", 1);
     }
     else
     {
         animator.SetFloat("movement", 0);
     }
    }

    private void MoveToCursor()
    {
        RaycastHit hit = GetPlayerMousePosition();
        
        GetComponent<NavMeshAgent>().destination = hit.point;
    }
    private RaycastHit GetPlayerMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }
}
