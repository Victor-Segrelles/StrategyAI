using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RangeEnemyCleric : NodeBT
{
    private Unit unit;

    public RangeEnemyCleric(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}