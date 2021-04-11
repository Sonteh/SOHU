﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNoRotate : MonoBehaviour
{
    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = rotation;
    }
}
