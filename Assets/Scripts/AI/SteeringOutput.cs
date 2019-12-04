using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringOutput
{
    public Vector3 velocity;
    public float rotation;

    public Vector3 linear; // a 2 or 3D vector
    public float angular; // a single floating point value

    public SteeringOutput(Vector3 velocity, float rotation, Vector3 linear, float angular)
    {
        this.velocity = velocity;
        this.rotation = rotation;
        this.linear = linear;
        this.angular = angular;
    }

    public SteeringOutput()
    {
        this.velocity = Vector3.zero;
        this.rotation = 0.0f;
        this.linear = Vector3.zero;
        this.angular = 0.0f;
    }

    public static SteeringOutput operator +(SteeringOutput a, SteeringOutput b)
    {
        a.velocity += b.velocity;
        a.rotation += b.rotation;
        a.linear += b.linear;
        a.angular += b.angular;

        return new SteeringOutput(a.velocity, a.rotation, a.linear, a.angular);
    }

    public SteeringOutput Weight(float weight)
    {
        linear *= weight;
        angular *= weight;

        return this;
    }

    public SteeringOutput Crop(Vector3 maxLinear, float maxAngular)
    {
        angular = angular > maxAngular ? maxAngular : angular;
        linear.x = linear.x > maxLinear.x ? maxLinear.x : linear.x;
        linear.y = linear.y > maxLinear.y ? maxLinear.y : linear.y;
        linear.z = linear.z > maxLinear.z ? maxLinear.z : linear.z;

        return this;
    }
}