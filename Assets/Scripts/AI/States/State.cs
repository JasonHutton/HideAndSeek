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

    public void GetAction()
    {
    }

    public void GetEntryAction()
    {

    }

    public void GetExitAction()
    {

    }
}
