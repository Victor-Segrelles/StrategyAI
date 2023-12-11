using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RangeEnemyArcher : NodeBT
{
    private Unit unit;

    public RangeEnemyArcher(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}