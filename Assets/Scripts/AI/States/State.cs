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

    public virtual void GetAction(AIInputController script)
    {
    }

    public virtual void GetEntryAction(AIInputController script)
    {

    }

    public virtual void GetExitAction(AIInputController script)
    {

    }
}
