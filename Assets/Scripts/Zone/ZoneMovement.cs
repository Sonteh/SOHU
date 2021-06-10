using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * GetComponent<ZoneData>().zoneSpeed * Time.deltaTime);
    }
}
