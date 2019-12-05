using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckShieldOnDecision : Decision
{
    public override DecisionTreeNode GetBranch()
    {
        if (testData == true) // Shield is on.
            return trueNode;
        else
            return falseNode; // Shield is off.
    }
}
