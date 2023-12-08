using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImAlive : NodeBT
{
    private int _life;

    public ImAlive(int life)
    {
        _life = life;
    }

    public override NodeState Evaluate()
    {
        int amount = (int)GetData("target");

        if (amount > 80)
        {
            Debug.Log("Tengo más de 80 de vida");
        }

        state = NodeState.RUNNING;
        return state;
    }

}