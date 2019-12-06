using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    public FleeState()
    {
    }
    public override void GetAction(AIInputController script)
    {
        script.steering += Flee.GetSteering(script.target._static.position, script.self._static.position, script.tank.maxSpeed).Weight(1.0f);

        if (!script.CheckShieldDanger())
            transitions[0].bTriggered = true;
    }
}
