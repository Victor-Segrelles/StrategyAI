using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckLife80 : NodeBT
{
    private int _life;

    public CheckLife80(int life)
    {
        _life = life;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {

            if (_life > 80)
            {
                Debug.Log(_life);
                parent.parent.SetData("target", _life);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
        Debug.Log(_life);
        state = NodeState.SUCCESS;
        return state;
    }

}