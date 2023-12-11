using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RangeEnemyWarrior : NodeBT
{
    private Unit unit;

    public RangeEnemyWarrior(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}