using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckRangeArcher : NodeBT
{
    private Unit unit;

    public CheckRangeArcher(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("CheckRangeArcher");
        if (t == null || unit.actionTarget == null)
        {
            Character odiado = null;
            float min = Mathf.Infinity;

            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange && ally.unit.life < min)
                {
                    odiado = ally;
                    min = ally.unit.life;
                }
            }


            if (odiado != null)
            {
                unit.actionTarget = odiado;
                unit.selectedAction = 1;    //Attack
                parent.parent.SetData("CheckRangeArcher", unit.actionTarget);
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