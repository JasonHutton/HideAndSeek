using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander3 : Kinematic
{
    public float maxSpeed;
    public float maxRotation;

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
            SteeringOutput output = GetSteering();

            rb.velocity = output.velocity;// rb.transform.forward * 10f;// rb.rotation.eulerAngles * 10f;// output.velocity;// rb.transform.forward;// * 1.0f * 20f * Time.fixedDeltaTime;
            //rb.rotation = Quaternion.AngleAxis(output.rotation, transform.up);// Quaternion.AngleAxis(output.rotation, transform.up);
        }
    }

    public SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        steering.velocity = maxSpeed * AngleToVector(this._static.orientation);
        //steering.rotation = RandomBinomial() * maxRotation;
        return steering;
    }

    public Vector3 AngleToVector(float angle)
    {
        // Mathf.Atan2(_velocity.x, _velocity.z) * Mathf.Rad2Deg;
        Vector3 v = Vector3.zero;
        v.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        v.z = Mathf.Sin(angle * Mathf.Deg2Rad);
        return v;
    }

    public float RandomBinomial()
    {
        return Random.value - Random.value;
    }
}
