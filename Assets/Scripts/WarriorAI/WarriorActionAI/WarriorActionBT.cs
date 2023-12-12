using System.Collections.Generic;
using BehaviorTree;

public class WarriorActionBT : Tree
{
    /*
    Warrior
  si varios enemigos a su alrededor: cleave
  slash ataca al enemigo con menos vida*/

    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckCleave(GetComponent<Unit>()),
                new Cleave(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckSlash(GetComponent<Unit>()),
                new Slash(),
            }),
        });
        return root;
    }
}