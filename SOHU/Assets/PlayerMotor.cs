using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    
    private Vector3 _velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move (Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void Rotate (Vector3 _rotation)
    {
        rotation = _rotation;
    }

    void FixedUpdate ()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement ()
    {
        if (_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation ()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
    }
}
