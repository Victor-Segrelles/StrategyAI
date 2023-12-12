using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class SelfHeal : NodeBT
{

    public SelfHeal()
    {

    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}