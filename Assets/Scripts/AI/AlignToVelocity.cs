﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToVelocity : UpdateOrientation
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
        this.orientationVector = this.velocity;
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
