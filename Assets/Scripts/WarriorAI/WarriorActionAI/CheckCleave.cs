using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckCleave : NodeBT
{
    private Unit unit;

    public CheckCleave(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkCleave");
        if (t == null || unit.AOETarget == null)
        {
            List<Character> enemigoscercanos = new List<Character>();

            foreach (Character enemy in unit.gm.allies)
            {
                
                if (Vector3.Distance(unit.gameObject.transform.position, enemy.gameObject.transform.position) < unit.attackRange)
                {
                    enemigoscercanos.Add(enemy);
                }
                
            }

            Vector3 puntoMedio = new Vector3(0,0,0);
            bool encontrado = false;

            if (enemigoscercanos.Count > 1)
            {
                encontrado = true;
            }

            if (encontrado)
            {
                unit.AOETarget = unit.gameObject.transform;
                unit.AOETarget.position = unit.gameObject.transform.position;   //Cuidado hay que coger el position no el AOETarget
                unit.selectedAction = 2;    //cleave
                parent.parent.SetData("checkCleave", unit.AOETarget);
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