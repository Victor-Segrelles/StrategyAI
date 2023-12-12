using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckArcane : NodeBT
{
    private Unit unit;

    public CheckArcane(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkArcane");
        if (t == null || unit.actionTargets == null)
        {
            Debug.Log("cafetera MALDITA");
            List<Character> odiados = new List<Character>();

            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange)
                {
                    odiados.Add(ally);
                    if (odiados.Count >= 3)
                    {
                        break;
                    }
                }
            }
            if (odiados.Count == 3)
            {
                unit.actionTargets = odiados;
                unit.selectedAction = 1;    //Attack
                parent.parent.SetData("checkArcane", unit.actionTargets);
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