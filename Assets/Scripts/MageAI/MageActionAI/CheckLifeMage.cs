using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLifeMage : NodeBT
{
    private Unit unit;

    public CheckLifeMage(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkLifeMage");
        if (t == null || (int)t != unit.life)
        {
            bool alguienCerca = false;

            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange)
                {
                    alguienCerca = true;
                }
            }

            if (unit.life <= 33 || !alguienCerca)
            {
                unit.selectedAction = 2;    //Shield
                parent.parent.SetData("checkLifeMage", unit.life);
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