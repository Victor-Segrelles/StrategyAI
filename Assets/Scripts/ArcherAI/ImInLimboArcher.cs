using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ImInLimboArcher : NodeBT
{
    
    public ImInLimboArcher()
    {
        
    }

    public override NodeState Evaluate()
    {
        Debug.Log("PENDEJOS TENGO 80");
        state = NodeState.RUNNING;
        return state;
    }

}