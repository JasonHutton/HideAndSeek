﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : Kinematic
{
    // Holds the static data for the character and target
    //public GameObject character;
    public Kinematic target;

    // Holds the maximum speed the character can travel
    public float maxSpeed;
    public bool flee; // Normally Seek. Enable this to flee.

    SteeringOutput getSteering()
    {
        // Create the structure for output
        SteeringOutput steering = new SteeringOutput();

        // Get the direction to the target
        if(!flee)
            steering.velocity = target._static.position - this._static.position; // Seek towards target.
        else
            steering.velocity = this._static.position - target._static.position; // Flee away from target.

        // The velocity is along this direction, at full speed
        steering.velocity.Normalize();
        steering.velocity *= maxSpeed;

        // Face in the direction we want to move
        //transform.eulerAngles = getNewOrientation(transform.eulerAngles, steering.velocity); // "Orientation"

        // Output the steering
        //steering.rotation = transform.eulerAngles.y;// character.transform.rotation; // O? 0? wat?

        return steering;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected virtual void FixedUpdate()
    {
        if (rb != null)
        {
            SteeringOutput output = getSteering();
            //Rigidbody rb = GetComponentInChildren<Rigidbody>();
            //transform.rotation.SetEulerAngles(0, output.rotation, 0);
            rb.velocity = output.velocity;
            //rb.velocity = output.velocity;
            //rb.rotation.SetEulerRotation(output.rotation);
        }
    }
}


