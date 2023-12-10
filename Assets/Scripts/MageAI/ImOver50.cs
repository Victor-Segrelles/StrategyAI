using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImOver50 : NodeBT
{
    private Unit unit;

    public ImOver50(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        int amount = (int)GetData("target");

        if (amount >= 50)
        {
            //Debug.Log("Tengo más de 80 de vida");
            Debug.Log(amount + "   Tengo 50 o más de vida");
        }

        state = NodeState.RUNNING;
        return state;
    }

}