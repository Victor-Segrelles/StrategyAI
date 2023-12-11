using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckOutOfRangeEnemyWarrior : NodeBT
{
    private Unit unit;

    public CheckOutOfRangeEnemyWarrior(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("outOfRange2Enemy");
        if (t == null || unit.attackTarget == null)
        {
            float min = Mathf.Infinity;
            Character minAllay = null;
            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < min)
                {
                    unit.attackTarget = ally;   //The enemy focus the ally
                    min = Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position);
                    minAllay = ally;
                }
            }
            if (minAllay != null)
            {
                parent.parent.SetData("outOfRange2Enemy", unit.attackTarget);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }

}