using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneMovement : MonoBehaviour
{

    private float f = 14.45E-2f;

    void Update()
    {
        transform.Translate(Vector3.forward * f * Time.deltaTime);
    }
}
