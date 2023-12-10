using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RangeEnemy : NodeBT
{
    private Unit unit;

    public RangeEnemy(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("sexo");
        state = NodeState.RUNNING;
        return state;
    }

}