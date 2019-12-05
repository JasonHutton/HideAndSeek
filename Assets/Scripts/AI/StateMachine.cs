﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    List<State> states;

    State initialState;
    State currentState;

    public StateMachine(List<State> _states)
    {
        // Got a problem here if _states.length == 0...
        states = _states;
        currentState = initialState = states[0];
    }

    void Update()
    {
        Transition triggeredTransition = null;

        foreach(Transition t in currentState.GetTransitions())
        {
            if(t.IsTriggered())
            {
                triggeredTransition = t;
                break;
            }
        }

        if(triggeredTransition != null)
        {
            State targetState = triggeredTransition.GetTargetState();

            currentState.GetExitAction();
            triggeredTransition.GetAction();
            targetState.GetEntryAction();
            /*
            # Add the exit action of the old state, the
            # transition action and the entry for the new state.
            actions = currentState.getExitAction()
            actions += triggeredTransition.getAction()
            actions += targetState.getEntryAction()
            */

            currentState = targetState;

            // return actions
        }
        else
        {
            currentState.GetAction();
        }

    }
}
