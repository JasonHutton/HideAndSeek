using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanEnableShieldDecision : Decision
{
    public override DecisionTreeNode GetBranch()
    {
        if(testData == false) // Sheild is off. We can turn it on!
            return trueNode;
        else
            return falseNode;
    }
}
