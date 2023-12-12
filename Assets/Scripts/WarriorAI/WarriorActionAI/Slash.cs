using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Slash : NodeBT
{

    public Slash()
    {

    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}