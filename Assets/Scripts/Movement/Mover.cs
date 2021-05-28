using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class Mover : NetworkBehaviour
{

    private Animator animator;

    public Rigidbody rb;
    CharacterController controller;
    NavMeshAgent player;

    private void Awake() {
        animator = GetComponentInChildren<Animator> ();
    }

    void Start()
    
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<NavMeshAgent>();
    }

    [ClientCallback]
    private void Update()
    {
        if (!isLocalPlayer) {return;}

        if (Input.GetButton("Fire2"))
        {
            MoveToCursor();
        }

        if(player.velocity != Vector3.zero) 
        {
            animator.SetFloat("movement", 0.5f);
        }
        else
        {
            animator.SetFloat("movement", 0);
        }

        if (Input.GetButton("Fireball"))
        {
            //animator.SetFloat("movement", 1);
            animator.SetTrigger("useSpell");
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
