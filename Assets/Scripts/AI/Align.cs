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

    void GetSteering()//Kinematic _target)
    {
        // Create the structure to hold our output
        SteeringOutput steering = new SteeringOutput();

        // Get the naive direction to the target
        // Map the result to the (-pi, pi) interval
        //rotation = mapToRange(target.orientation - this.orientation);
        //float rotationSize = Mathf.Abs(rotationDirection);
    }
}

    /*

28
29 # Map the result to the (-pi, pi) interval
30 rotation = mapToRange(rotation)
31 rotationSize = abs(rotationDirection)
32
33 # Check if we are there, return no steering
68 Chapter 3 Movement
34 if rotationSize<targetRadius
35 return None
36
37 # If we are outside the slowRadius, then use
38 # maximum rotation
39 if rotationSize> slowRadius:
40 targetRotation = maxRotation
41
42 # Otherwise calculate a scaled rotation
43 else:
44 targetRotation =
45 maxRotation* rotationSize / slowRadius
46
47 # The final target rotation combines
48 # speed (already in the variable) and direction
49 targetRotation *= rotation / rotationSize
50
51 # Acceleration tries to get to the target rotation
52 steering.angular =
53 targetRotation - character.rotation
54 steering.angular /= timeToTarget
55
56 # Check if the acceleration is too great
57 angularAcceleration = abs(steering.angular)
58 if angularAcceleration > maxAngularAcceleration:
59 steering.angular /= angularAcceleration
60 steering.angular *= maxAngularAcceleration
61
62 # Output the steering
63 steering.linear = 0
64 return steering
}
*/