using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToTarget2
{
    public static SteeringOutput GetSteering(Vector3 target, Vector3 position, float currentOrientation)
    {
        SteeringOutput steering = new SteeringOutput();

        AlignTowardsTarget(ref steering, target, position, currentOrientation);

        // This is a bunch of hacky stuff to make the numbers work out.
        // I'd really rather just use quaternion functions for everything here...
        steering.rotation += 180;
        if (steering.rotation - currentOrientation < -180)
            steering.rotation += 360;
        if (steering.rotation - currentOrientation > 180)
            steering.rotation -= 360;

        //Debug.Log(string.Format("current: {0} rotation: {1} combined {2}", currentOrientation, steering.rotation, steering.rotation - currentOrientation));
        steering.rotation -= currentOrientation;

        return steering;
    }

    private static void AlignTowardsTarget(ref SteeringOutput steering, Vector3 target, Vector3 position, float currentOrientation)
    {
        Vector3 temp = target - position; // Direction to target
        steering.rotation = GetNewOrientation(temp, currentOrientation);
    }

    public static float GetNewOrientation(Vector3 targetVector, float currentOrientation)
    {
        // Make sure we have a velocity
        if (targetVector.magnitude > 0.0f)
        {
            // Calculate orientation using an arc tangent of the velocity components.
            return Mathf.Atan2(-targetVector.x, -targetVector.z) * Mathf.Rad2Deg;
        }
        else
        {
            return currentOrientation;
        }
    }
}
