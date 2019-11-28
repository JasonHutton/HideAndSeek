using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOrientation : Kinematic
{
    public Vector3 orientationVector;

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

    protected void FixedUpdate()
    {
        if (rb != null)
        {
            rb.rotation = Quaternion.AngleAxis(GetNewOrientation(orientationVector), transform.up); // This is actually correct in testing due to how player movement is. Reversing swaps forward, so swaps the next movement, so it oscillates when 'reversing'
        }
    }

    public float GetNewOrientation(Vector3 targetVector)
    {
        // Make sure we have a velocity
        if (targetVector.magnitude > 0.0f)
        {
            // Calculate orientation using an arc tangent of the velocity components.
            return Mathf.Atan2(targetVector.x, targetVector.z) * Mathf.Rad2Deg;
        }
        else
        {
            return this._static.orientation;
        }
    }
}
