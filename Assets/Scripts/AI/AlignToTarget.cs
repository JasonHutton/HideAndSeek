using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToTarget : UpdateOrientation
{
    public Kinematic target;
    public bool alignAwayFromTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target != null)
        {
            if(!alignAwayFromTarget)
                this.orientationVector = target._static.position - this._static.position; // Align towards target
            else
                this.orientationVector = this._static.position - target._static.position; // Align away from target.
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
