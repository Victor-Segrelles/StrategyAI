using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLife80 : NodeBT
{
    private Unit unit;

    public CheckLife80(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(unit.life);
        object t = GetData("target");
        if(t==null || (int)t!=unit.life){

            if (unit.life > 80)
            {
                Debug.Log(unit.life);
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