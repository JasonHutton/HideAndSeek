using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGunReadyDecision : Decision
{
    public override DecisionTreeNode GetBranch()
    {
        if (testData == true) 
            return trueNode;  // Gun is ready to shoot
        else
            return falseNode; // Gun is not ready to shoot
    }
}
