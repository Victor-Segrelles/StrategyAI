using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImUnder50Warrior : NodeBT
{
    private Unit unit;

    public ImUnder50Warrior(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        int amount = (int)GetData("target");

        if (amount < 30)
        {
            Debug.Log("NOOOO TOY MUERTOOOOOOO"+amount);
        }

        state = NodeState.RUNNING;
        return state;
    }

}