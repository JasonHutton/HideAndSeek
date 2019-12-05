using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public static SteeringOutput GetSteering(Vector3 target, Vector3 position, float speed)
    {
        // Create the structure for output
        SteeringOutput steering = new SteeringOutput();

        SeekDirection(ref steering, target, position);

        // The velocity is along this direction, at full speed
        steering.velocity.Normalize();
        steering.velocity *= speed;

        return steering;
    }

    private static void SeekDirection(ref SteeringOutput steering, Vector3 target, Vector3 position)
    {
        // Get the direction to the target
        steering.velocity = target - position; // Seek towards target.
    }
}


