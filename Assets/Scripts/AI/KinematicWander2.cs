using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander2 : Kinematic
{
    // Holds the static data for the character
    //character

    // Holds the maximum speed the character can travel
    public float maxSpeed;

    // Holds the maximum rotation speed we’d like, probably should be smaller than the maximum possible, to allow
    // a leisurely change in direction
    public float maxRotation;

    private float wanderChangeTimer;
    public float wanderChangeInterval;

    SteeringOutput getSteering()
    {
        // Create the structure for output
        SteeringOutput steering = new SteeringOutput();

        // Get velocity from the vector form of the orientation
        steering.velocity = maxSpeed * AngleToVector(this.orientation);//this.orientation.asVector()

        // Change our orientation randomly
        steering.rotation = randomBinomial() * maxRotation;

        // Output the steering
        return steering;
    }

    public float randomBinomial()
    {
        return Random.value - Random.value;
    }

    public Vector3 AngleToVector(float angle)
    {
        // Mathf.Atan2(_velocity.x, _velocity.z) * Mathf.Rad2Deg;
        Vector3 v = Vector3.zero;
        v.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        v.z = Mathf.Sin(angle * Mathf.Deg2Rad);
        return v;
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        wanderChangeTimer = -1.0f; // Start below 0.
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    // DON'T FORGET FACING AND VELOCITY SHOULD BE INDEPENDENT!
    private void FixedUpdate()
    {
        if (rb != null)
        {
            SteeringOutput output = new SteeringOutput();
            if (Time.fixedTime > wanderChangeTimer)
            {
                wanderChangeTimer = Time.fixedTime + wanderChangeInterval;
                output = getSteering();
                // Are these updating this.rotation and this.velocity? no? why?
            }
            else
            {
                output.rotation = this.rotation;
                output.velocity = this.velocity;
            }

            rb.velocity = output.velocity;// rb.transform.forward * 10f;// rb.rotation.eulerAngles * 10f;// output.velocity;// rb.transform.forward;// * 1.0f * 20f * Time.fixedDeltaTime;
            rb.rotation = Quaternion.AngleAxis(output.rotation, transform.up) ;// Quaternion.AngleAxis(output.rotation, transform.up);
        }
    }
}
