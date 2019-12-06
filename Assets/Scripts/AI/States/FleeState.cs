﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    public FleeState()
    {
        Debug.Log("FleeConstructor");
    }
    public void GetAction(AIInputController2 script)
    {
        Debug.Log("FleeState");
    }
}