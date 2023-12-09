using System.Collections.Generic;
using BehaviorTree;

public class MageBT : Tree
{
    public int life;

    protected override NodeBT SetupTree()
    {
        UnityEngine.Debug.Log(life);
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLife80(GetComponent<Unit>()),
                new ImAlive(GetComponent<Unit>()),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckDead80(GetComponent<Unit>()),
                new ImDead(GetComponent<Unit>()),
            }),
            new ImInLimbo(),
        });

        return root;
    }
}