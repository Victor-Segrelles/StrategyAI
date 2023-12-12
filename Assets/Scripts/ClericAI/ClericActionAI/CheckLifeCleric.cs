using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLifeCleric : NodeBT
{
    private Unit unit;

    public CheckLifeCleric(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkLifeCleric");
        if (t == null || (int)t != unit.life)
        {

            if (unit.life <= 50)
            {
                unit.actionTarget = unit.gameObject.GetComponent<Character>();
                unit.selectedAction = 2;    //Heal
                parent.parent.SetData("checkLifeCleric", unit.actionTarget);
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