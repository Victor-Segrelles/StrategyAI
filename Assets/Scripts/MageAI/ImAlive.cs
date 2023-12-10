using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImAlive : NodeBT
{
    private Unit unit;

    public ImAlive(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        int amount = (int)GetData("target");

        if (amount > 80)
        {
            //Debug.Log("Tengo m�s de 80 de vida");
            Debug.Log("Tengo m�s de 80 de vida"+amount);
        }

        state = NodeState.RUNNING;
        return state;
    }

}