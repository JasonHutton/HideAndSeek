using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : Kinematic
{
    // Holds the static data for the character and target
    //public GameObject character;
    public Kinematic target;

    // Holds the maximum speed the character can travel
    public float maxSpeed;

    public struct KinematicSteeringOutput
    {
        public Vector3 velocity;
        //public Quaternion rotation;
        //public Vector3 rotation;
        public float rotation;
    }

    KinematicSteeringOutput getSteering()
    {
        // Create the structure for output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Get the direction to the target
        steering.velocity = target.position - transform.position;

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
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            KinematicSteeringOutput output = getSteering();
            //Rigidbody rb = GetComponentInChildren<Rigidbody>();
            //transform.rotation.SetEulerAngles(0, output.rotation, 0);
            rb.velocity = output.velocity;
            //rb.velocity = output.velocity;
            //rb.rotation.SetEulerRotation(output.rotation);
        }
    }
}


