using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee
{
    public static SteeringOutput GetSteering(Vector3 target, Vector3 position, float speed)
    {
        // Create the structure for output
        SteeringOutput steering = new SteeringOutput();

        FleeDirection(ref steering, target, position);

        // The velocity is along this direction, at full speed
        steering.velocity.Normalize();
        steering.velocity *= speed;

        return steering;
    }

    private static void FleeDirection(ref SteeringOutput steering, Vector3 target, Vector3 position)
    {
        // Get the direction to the target
        steering.velocity = position - target; // Flee away from target.
    }
}


