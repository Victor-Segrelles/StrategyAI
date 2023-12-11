using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class OutOfRangeEnemyCleric : NodeBT
{
    private Unit unit;

    public OutOfRangeEnemyCleric(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(unit.myturn);
        if (!unit.moved)
        {
            unit.move2(unit.attackTarget.gameObject.transform);
        }

        state = NodeState.RUNNING;
        return state;
    }

}