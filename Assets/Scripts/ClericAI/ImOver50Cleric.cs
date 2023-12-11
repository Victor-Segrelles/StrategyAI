using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImOver50Cleric : NodeBT
{
    private Unit unit;

    public ImOver50Cleric(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        int amount = (int)GetData("target");

        if (amount >= 50)
        {

        }

        state = NodeState.RUNNING;
        return state;
    }

}