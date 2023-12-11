using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLifeOver50Archer : NodeBT
{
    private Unit unit;

    public CheckLifeOver50Archer(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t==null || (int)t!=unit.life){

            if (unit.life >= 50)
            {
                //Debug.Log(unit.life + "Tengo 50 o m�s de vida");
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