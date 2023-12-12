using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class AOEHeal : NodeBT
{

    public AOEHeal()
    {

    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}