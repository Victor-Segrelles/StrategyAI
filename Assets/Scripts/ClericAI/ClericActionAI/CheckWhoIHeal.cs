using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckWhoIHeal : NodeBT
{
    private Unit unit;

    public CheckWhoIHeal(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkWhoIHeal");
        if (t == null || unit.actionTarget == null)
        {
            Character necesitado = null;
            float min = Mathf.Infinity;

            foreach (Character ally in unit.gm.enemies)
            {
                if (ally.unit.life < 100)
                {
                    if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange && ally.unit.life < min)
                    {
                        necesitado = ally;
                        min = ally.unit.life;
                    }
                }
            }


            if (necesitado != null)
            {
                unit.actionTarget = necesitado;
                unit.selectedAction = 2;    //Heal
                parent.parent.SetData("checkWhoIHeal", unit.actionTarget);
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