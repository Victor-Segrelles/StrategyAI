using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTwinShot : NodeBT
{
    private Unit unit;

    public CheckTwinShot(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("CheckTwinShot");
        if (t == null || unit.actionTargets == null)
        {
            Debug.Log("cafetera putrefacta");
            List<Character> odiados = new List<Character>();

            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange)
                {
                    odiados.Add(ally);
                    if(odiados.Count>=2){
                        break;
                    }
                }
            }
            if (odiados.Count==2)
            {
                unit.actionTargets = odiados;
                unit.selectedAction = 2;    //Attack
                parent.parent.SetData("CheckTwinShot", unit.actionTargets);
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