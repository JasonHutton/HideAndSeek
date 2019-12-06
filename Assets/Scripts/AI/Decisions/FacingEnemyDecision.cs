using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingEnemyDecision : Decision
{
    public float testData;
    public override DecisionTreeNode GetBranch()
    {
        if (testData < 10.0f)
            return trueNode;  // We're facing the enemy
        else
            return falseNode; // We're not facing the enemy
    }
}
