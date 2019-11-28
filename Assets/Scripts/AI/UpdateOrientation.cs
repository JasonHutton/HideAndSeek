using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOrientation : Kinematic
{
    public Vector3 orientationVector;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected virtual void FixedUpdate()
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

/*
 * 
 Blaaaaah?
 * class Face (Align):
2
3 # Overrides the Align.target member
4 target
5
6 # ... Other data is derived from the superclass ...
7
8 # Implemented as it was in Pursue
9 def getSteering():
10
11 # 1. Calculate the target to delegate to align
12
13 # Work out the direction to target
14 direction = target.position - character.position
15
16 # Check for a zero direction, and make no change if so
17 if direction.length() == 0: return target
18
19 # Put the target together
20 Align.target = explicitTarget
21 Align.target.orientation = atan2(-direction.x, direction.z)
22
23 # 2. Delegate to align
24 return Align.getSteering()
 * */
