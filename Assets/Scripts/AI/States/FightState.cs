using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : State
{
    public FightState()
    {
        Debug.Log("FightConstructor");
    }
    public void GetAction(AIInputController2 script)
    {
        Debug.Log("FightState");
    }
}
