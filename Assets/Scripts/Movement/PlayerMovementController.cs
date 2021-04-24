using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementController : NetworkBehaviour
{
    private float previousInput;

    private static Controls controls;
    public static Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;

        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<float>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
    }

    [ClientCallback]
    private void Update()
    {
        Move();
        UpdateAnimator();
    } 

    [Client]
    private void SetMovement(float movement) => previousInput = movement;

    [Client]
    private void ResetMovement() => previousInput = 0;

    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    [Client]
    private void Move()
    {
        if (previousInput == 1f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            var offset = hit.point - transform.position;

            if (hasHit && offset.magnitude > 1.5f)
            {
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }
    }

    [Client]
    private void UpdateAnimator()
    {
        Vector3 velocity =  GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}