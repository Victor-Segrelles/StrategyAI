using System.Collections.Generic;
using BehaviorTree;

public class ArcherActionBT : Tree
{
    /*
    ARCHER
    si vida crítica: EVADE
    si hay 2 enemigos: TWIN SHOT
    si hay 1 enemigo: SINGLE SHOT
    */
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeArcher(GetComponent<Unit>()),
                new SelfHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckTwinShot(GetComponent<Unit>()),
                new AOEHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckSingleShot(GetComponent<Unit>()),
                new WhoIHeal(),
            }),
        });
        return root;
    }
}