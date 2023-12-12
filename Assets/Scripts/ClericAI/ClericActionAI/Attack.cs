using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Attack : NodeBT
{

    public Attack()
    {

    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}