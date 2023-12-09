using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImDead : NodeBT
{
    private int _life;

    public ImDead(int life)
    {
        _life = life;
    }

    public override NodeState Evaluate()
    {
        int amount = (int)GetData("target");

        if (amount < 80)
        {
            Debug.Log("NOOOO TOY MUERTOOOOOOO");
        }

        state = NodeState.RUNNING;
        return state;
    }

}