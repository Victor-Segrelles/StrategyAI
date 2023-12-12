using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLifeArcher : NodeBT
{
    private Unit unit;

    public CheckLifeArcher(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkLifeArcher");
        if (t == null || (int)t != unit.life)
        {

            if (unit.life <= 40)
            {
                unit.selectedAction = 3;    //evade
                parent.parent.SetData("checkLifeArcher", unit.life);
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