using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : State
{
    public FightState()
    {
    }
    public override void GetAction(AIInputController script)
    {
        script.steering += Seek.GetSteering(script.target._static.position, script.self._static.position, script.tank.maxSpeed).Weight(1.0f);
    }
}
