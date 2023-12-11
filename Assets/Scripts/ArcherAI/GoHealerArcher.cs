using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GoHealerArcher : NodeBT
{
    private Unit unit;

    public GoHealerArcher(Unit obj)
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