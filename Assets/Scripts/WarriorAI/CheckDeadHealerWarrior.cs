using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckDeadHealerWarrior : NodeBT
{
    private Unit unit;

    public CheckDeadHealerWarrior(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("deadheal");
        if (t == null || unit.attackTarget == null)
        {

            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange)
                {
                    Debug.Log(Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position));
                    unit.attackTarget = ally;   //The enemy focus the ally
                }
            }
            if (unit.attackTarget != null)
            {
                parent.parent.SetData("range2Enemy", unit.attackTarget);
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