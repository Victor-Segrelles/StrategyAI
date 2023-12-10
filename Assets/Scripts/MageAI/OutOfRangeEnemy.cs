using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class OutOfRangeEnemy : NodeBT
{
    private Unit unit;

    public OutOfRangeEnemy(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        if (!unit.moved)
        {
            unit.move2(unit.attackTarget.gameObject.transform);
        }

        state = NodeState.RUNNING;
        return state;
    }

}