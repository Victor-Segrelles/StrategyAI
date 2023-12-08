using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckDead80 : NodeBT
{
    private int _life;

    public CheckDead80(int life)
    {
        _life = life;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {

            if (_life < 80)
            {
                parent.parent.SetData("target", _life);
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