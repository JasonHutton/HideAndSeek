using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToTarget : UpdateOrientation
{
    public Kinematic target;

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
            this.orientationVector = target._static.position - this._static.position;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
