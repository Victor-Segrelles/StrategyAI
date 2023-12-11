using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckHealerCleric : NodeBT
{
    private Unit unit;

    public CheckHealerCleric(Unit obj)
    {
        unit=obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("healer");
        if (t == null || unit.attackTarget == null)
        {

            foreach (Character enem in unit.gm.enemies)
            {
                if (enem is Cleric)
                {
                    //Debug.Log(Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position));
                    unit.attackTarget = enem;   //The enemy focus the ally
                }
            }
            if (unit.attackTarget != null)
            {
                parent.parent.SetData("healer", unit.attackTarget);
                state = NodeState.SUCCESS;
                return state;
            }
            UnityEngine.Debug.Log("healer muertisimo");
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}