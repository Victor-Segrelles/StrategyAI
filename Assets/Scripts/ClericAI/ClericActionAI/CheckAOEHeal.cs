using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckAOEHeal : NodeBT
{
    private Unit unit;

    public CheckAOEHeal(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkAOEHeal");
        if (t == null || unit.AOETarget == null)
        {
            List<Character> necesitadosCercanos = new List<Character>();

            foreach (Character ally in unit.gm.enemies)
            {
                if (ally.unit.life < 100)
                {
                    if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange)
                    {
                        necesitadosCercanos.Add(ally);
                    }
                }
            }

            Vector3 puntoMedio = new Vector3(0,0,0);
            bool encontrado = false;

            if (necesitadosCercanos.Count > 1)
            {
                
                
                foreach (Character ally in necesitadosCercanos)
                {
                    foreach (Character ally2 in necesitadosCercanos)
                    {
                        if (ally != ally2 && Vector3.Distance(ally.transform.position, ally2.transform.position) < 10)  //10 es el diámetro del area
                        {
                            puntoMedio = (ally.transform.position + ally2.transform.position) / 2f;
                            encontrado = true;
                            break;
                        }
                    }
                    if (encontrado)
                    {
                        break;
                    }
                }
            }

            if (encontrado)
            {
                unit.AOETarget = unit.gameObject.transform;
                unit.AOETarget.position = puntoMedio;   //Cuidado hay que coger el position no el AOETarget
                unit.selectedAction = 3;    //AOEHeal
                parent.parent.SetData("checkAOEHeal", unit.AOETarget);
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