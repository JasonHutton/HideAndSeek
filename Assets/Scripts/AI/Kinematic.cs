using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Vector3 position; // a 2 or 3D vector
    public float orientation; // a single floating point value
    public Vector3 velocity; // another 2 or 3D vector
    public float rotation; // a single floating point value

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = GetComponentInChildren<Rigidbody>();
        }
    }

    void Update()
    {
        if (rb != null)
        {
            position = rb.position;
            orientation = rb.rotation.eulerAngles.y;
            velocity = rb.velocity;
            rotation = rb.angularVelocity.y; // Not sure about that...

            if (velocity.magnitude > 0.0f)
            {
                float temp = getNewOrientation(orientation, velocity);
                rb.rotation = Quaternion.AngleAxis(temp, transform.up);
                //rb.angularVelocity = transform.up * temp;
                //rb.MoveRotation(Quaternion.AngleAxis(temp, Vector3.up));
                //.rb.rotation.eulerAngles.Set(0, temp, 0);
            }
        }
    }

    // force the orientation of a character to be in the direction it is travelling
    float getNewOrientation(float currentOrientation, Vector3 _velocity)
    {
        // Make sure we have a velocity
        if (_velocity.magnitude > 0.0f)
        {
            if (orientation == 270)
            {
                int q = 0;
                q++;
            }
            // Calculate orientation using an arc tangent of the velocity components.
            //return Vector3.
            //return Mathf.Atan2(_velocity.z, _velocity.x) * Mathf.Rad2Deg;
            return Mathf.Atan2(_velocity.x, _velocity.z) * Mathf.Rad2Deg;

        }
        else
        {
            return currentOrientation;
        }
    }
}