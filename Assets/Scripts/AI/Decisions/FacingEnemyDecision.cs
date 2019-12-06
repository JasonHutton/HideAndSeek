using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingEnemyDecision : Decision
{
    public override DecisionTreeNode GetBranch()
    {
        if (testData == true)
            return trueNode;  // We're facing the enemy
        else
            return falseNode; // We're not facing the enemy
    }
}
