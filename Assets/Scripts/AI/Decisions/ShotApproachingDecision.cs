using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotApproachingDecision : Decision
{
    public float testData;
    public override DecisionTreeNode GetBranch()
    {
        if (testData < 8.0f) // Distance to nearest shot on approach.
            return trueNode;
        else
            return falseNode;
    }
}
