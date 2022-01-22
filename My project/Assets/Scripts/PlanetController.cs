using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Planet physics logic
 */

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float planetRotationSpeed;
    
    private void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0, 0, planetRotationSpeed);
    }
}
