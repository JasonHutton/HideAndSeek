using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : Kinematic
{
    // Holds the kinematic data for the character and target character
    public Kinematic target;

    // Holds the max angular acceleration and rotation of the character
    public float maxAngularAcceleration;
    public float maxRotation;

    // Holds the radius for arriving at the target
    public float targetRadius;

    // Holds the radius for beginning to slow down
    public float slowRadius;

    // Holds the time over which to achieve target speed
    public float timeToTarget;

    // Start is called before the first frame update
    void Start()
    {
        timeToTarget = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    func(align* Align) mapToRange(rotation float64) float64 {
	for rotation< -math.Pi {
		rotation += math.Pi* 2
	}
	for rotation > math.Pi {
		rotation -= math.Pi* 2
	}
	return rotation
}
*/
    float mapToRange(float orientation)
    {
        while(orientation < -Mathf.PI)
        {
            orientation += Mathf.PI * 2;
        }
        while(orientation > Mathf.PI * 2)
        {
            orientation -= Mathf.PI * 2;
        }

        return orientation;
    }

    void GetSteering()//Kinematic _target)
    {
        // Create the structure to hold our output
        SteeringOutput steering = new SteeringOutput();

        // Get the naive direction to the target
        // Map the result to the (-pi, pi) interval
        rotation = mapToRange(target.orientation - this.orientation);
        float rotationSize = Mathf.Abs(rotationDirection);
        // Map the result to the (-pi, pi) interval
        rotation = mapToRange(rotation)
        rotationSize = abs(rotationDirection)

        // Check if we are there, return no steering
        if rotationSize < targetRadius
            return None

        // If we are outside the slowRadius, then use maximum rotation
        if rotationSize > slowRadius:
            targetRotation = maxRotation

        // Otherwise calculate a scaled rotation
        else:
            targetRotation =
            maxRotation* rotationSize / slowRadius

        // The final target rotation combines speed (already in the variable) and direction
        targetRotation *= rotation / rotationSize

        // Acceleration tries to get to the target rotation
        steering.angular =
        targetRotation - character.rotation
        steering.angular /= timeToTarget

        // Check if the acceleration is too great
        angularAcceleration = abs(steering.angular)
        if angularAcceleration > maxAngularAcceleration:
            steering.angular /= angularAcceleration
            steering.angular *= maxAngularAcceleration

        // Output the steering
        steering.linear = 0
        return steering
    }
}
