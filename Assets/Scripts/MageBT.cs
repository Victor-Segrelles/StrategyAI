using System.Collections.Generic;
using BehaviorTree;

public class MageBT : Tree
{
    public int life;

    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLife80(life),
                new ImAlive(life),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckDead80(life),
                new ImDead(life),
            })
        });

        return root;
    }
}