using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : DecisionTreeNode
{
    public DecisionTreeNode trueNode;
    public DecisionTreeNode falseNode;
    public bool testData; // data that forms the basis of the test

    public override DecisionTreeNode MakeDecision()
    {
        return GetBranch().MakeDecision();
    }

    public abstract DecisionTreeNode GetBranch();
}
