﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToVelocity : UpdateOrientation
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        this.orientationVector = this.velocity;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
