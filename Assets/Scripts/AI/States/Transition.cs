using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public State targetState;
    public bool bTriggered;

    public bool IsTriggered()
    {
        return bTriggered;
    }

    public State GetTargetState()
    {
        return targetState;
    }

    public void GetAction()
    {

    }
}
