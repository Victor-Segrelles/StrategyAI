using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Cleave : NodeBT
{

    public Cleave()
    {

    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}