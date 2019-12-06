using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected List<Transition> transitions;

    public State()
    {
        transitions = new List<Transition>();
    }

    public List<Transition> GetTransitions()
    {
        return transitions;
    }

    public void GetAction(AIInputController2 script)
    {
    }

    public void GetEntryAction(AIInputController2 script)
    {

    }

    public void GetExitAction(AIInputController2 script)
    {

    }
}
