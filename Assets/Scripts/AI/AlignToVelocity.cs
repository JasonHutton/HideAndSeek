using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToVelocity : Kinematic
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (rb != null)
        {
            rb.rotation = Quaternion.AngleAxis(getNewOrientation(orientation, velocity), transform.up); // This is actually correct in testing due to how player movement is. Reversing swaps forward, so swaps the next movement, so it oscillates when 'reversing'
        }
    }

    // force the orientation of a character to be in the direction it is travelling
    float getNewOrientation(float currentOrientation, Vector3 _velocity)
    {
        // Make sure we have a velocity
        if (_velocity.magnitude > 0.0f)
        {
            // Calculate orientation using an arc tangent of the velocity components.
            return Mathf.Atan2(_velocity.x, _velocity.z) * Mathf.Rad2Deg;
        }
        else
        {
            return currentOrientation;
        }
    }
}
