using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class WhoIHeal : NodeBT
{

    public WhoIHeal()
    {

    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}