using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckAOEFirestorm : NodeBT
{
    private Unit unit;

    public CheckAOEFirestorm(Unit obj)
    {
        unit = obj;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("checkAOEFirestorm");
        if (t == null || unit.AOETarget == null)
        {
            List<Character> odiadosCercanos = new List<Character>();

            foreach (Character ally in unit.gm.allies)
            {
                if (Vector3.Distance(unit.gameObject.transform.position, ally.gameObject.transform.position) < unit.attackRange)
                {
                    odiadosCercanos.Add(ally);
                }
            }

            Vector3 puntoMedio = new Vector3(0, 0, 0);
            bool encontrado = false;

            if (odiadosCercanos.Count > 1)
            {


                foreach (Character ally in odiadosCercanos)
                {
                    foreach (Character ally2 in odiadosCercanos)
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
            else if (odiadosCercanos.Count >= 1 && !encontrado)
            {
                puntoMedio = odiadosCercanos[0].transform.position;
                encontrado = true;
            }

            if (encontrado)
            {
                unit.AOETarget = unit.gameObject.transform;
                unit.AOETarget.position = puntoMedio;   //Cuidado hay que coger el position no el AOETarget
                unit.selectedAction = 1;    //AOEHeal
                parent.parent.SetData("checkAOEFirestorm", unit.AOETarget);
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