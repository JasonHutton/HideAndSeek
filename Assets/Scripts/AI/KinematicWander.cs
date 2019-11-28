using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : Kinematic
{
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
        //steering.velocity = maxSpeed * AngleToVector(this.orientation);
        Vector3 pos1 = AngleToVector(this._static.orientation);
        Vector3 pos2 = rb.transform.forward;
        //Vector3 pos1f = AlignToVelocity.getNewOrientation(Quaternion.AngleAxis(this.orientation, transform.up), pos1);

        steering.velocity = AngleToVector(this._static.orientation);// rb.transform.forward;// Vector3.zero - this.position;
        // The velocity is along this direction, at full speed
        steering.velocity.Normalize();
        steering.velocity *= maxSpeed;


        float ang1 = this.rotation;
        Vector3 ang2 = rb.transform.rotation.eulerAngles;
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
        wanderChangeTimer = -1.0f; // Start it below 0, so it gets adjusted on the first iteration.
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

            //Rigidbody rb = GetComponentInChildren<Rigidbody>();
            //transform.rotation.SetEulerAngles(0, output.rotation, 0);
            rb.velocity = output.velocity;// rb.transform.forward * 10f;// rb.rotation.eulerAngles * 10f;// output.velocity;// rb.transform.forward;// * 1.0f * 20f * Time.fixedDeltaTime;
            rb.rotation = Quaternion.AngleAxis(output.rotation, transform.up);
            //rb.velocity = output.velocity;
            //rb.rotation.SetEulerRotation(output.rotation);
        }
    }
}
