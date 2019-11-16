using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{
    // Holds the static data for the character and target
    GameObject character;
    GameObject target;

    // Holds the maximum speed the character can travel
    public float maxSpeed;

    public struct KinematicSteeringOutput
    {
        public Vector3 velocity;
        public Quaternion rotation;
    }

    KinematicSteeringOutput getSteering()
    {
        // Create the structure for output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Get the direction to the target
        steering.velocity = target.transform.position - character.transform.position;

        // The velocity is along this direction, at full speed
        steering.velocity.Normalize();
        steering.velocity *= maxSpeed;

        // Face in the direction we want to move
        character.transform.eulerAngles = getNewOrientation(character.transform.eulerAngles, steering.velocity); // "Orientation"

        // Output the steering
        steering.rotation = character.transform.rotation; // O? 0? wat?

        return steering;
    }

    Vector3 getNewOrientation(Vector3 currentOrientation, Vector3 currentVelocity)
    {

        // Make sure we have a velocity
        if (Vector3.Magnitude(currentVelocity) > 0)
        {
            // Calculate orientation using an arc tangent of the velocity components.
            //return Mathf.Atan2(-currentVelocity.x, currentVelocity.z);
            return Vector3.Normalize(currentVelocity);
        }
        // Otherwise use the current orientation
        else
        {
            return Vector3.Normalize(transform.rotation.eulerAngles);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}


