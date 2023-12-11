using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class OutOfRangeEnemyArcher : NodeBT
{
    private Unit unit;

    public OutOfRangeEnemyArcher(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("cafetera oxidada");
        if (!unit.moved)
        {
            unit.move2(unit.attackTarget.gameObject.transform);
        }

        state = NodeState.RUNNING;
        return state;
    }

}