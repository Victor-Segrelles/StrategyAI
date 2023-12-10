using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLifeUnder50 : NodeBT
{
    private Unit unit;

    public CheckLifeUnder50(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t==null || (int)t!=unit.life){
        

            if (unit.life < 50)
            {
                parent.parent.SetData("target", unit.life);
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