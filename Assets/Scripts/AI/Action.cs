using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : DecisionTreeNode
{
    public override DecisionTreeNode MakeDecision()
    {
        return this;
    }

    public void GetExitAction()
    {

    }

    public void GetAction()
    {

    }

    public void GetEntryAction()
    {

    }
}
