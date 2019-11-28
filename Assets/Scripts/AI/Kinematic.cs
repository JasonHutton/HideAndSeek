using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Static _static;
    public Vector3 velocity; // another 2 or 3D vector
    public float rotation; // a single floating point value

    protected Rigidbody rb;

    protected void Start()
    {
        _static = new Static();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = GetComponentInChildren<Rigidbody>();
        }
    }

    protected void Update()
    {
        if (rb != null)
        {
            _static.position = rb.position;
            _static.orientation = rb.rotation.eulerAngles.y;
            velocity = rb.velocity;
            rotation = rb.angularVelocity.y; // Not sure about that...
        }
    }


}