using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Vector3 position; // a 2 or 3D vector
    public float orientation; // a single floating point value
    public Vector3 velocity; // another 2 or 3D vector
    public float rotation; // a single floating point value

    protected Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = GetComponentInChildren<Rigidbody>();
        }
    }

    void Update()
    {
        if (rb != null)
        {
            position = rb.position;
            orientation = rb.rotation.eulerAngles.y;
            velocity = rb.velocity;
            rotation = rb.angularVelocity.y; // Not sure about that...
        }
    }


}