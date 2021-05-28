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
        test = GetComponent<NavMeshAgent>();
    }

    [ClientCallback]
    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetButton("Fire2"))
        {
            MoveToCursor();
        }

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
