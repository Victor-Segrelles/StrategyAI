using System.Collections.Generic;
using BehaviorTree;

public class MageActionBT : Tree
{
    /*
    MAGE
    si vida crítica: SHIELD
    si no hay enemigo a rango y no se tiene escudo: SHIELD
    (Decidir objetivo: vida crítica > healer > otros)
    si hay varios enemigos cercanos entre ellos: FIRESTORM
    si hay 3 enemigos: ARCANE MISSILES
    si hay 1 enemigo: FIRESTORM*/
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeMage(GetComponent<Unit>()),
                new SelfHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckArcane(GetComponent<Unit>()),
                new WhoIHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckAOEFirestorm(GetComponent<Unit>()),
                new AOEHeal(),
            }),
        });
        return root;
    }
}