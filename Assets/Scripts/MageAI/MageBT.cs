using System.Collections.Generic;
using BehaviorTree;

public class MageBT : Tree
{
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeOver50(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckRangeEnemy(GetComponent<Unit>()),
                        new RangeEnemy(GetComponent<Unit>()),
                    }),
                    new Sequence(new List<NodeBT>
                    {
                        new CheckOutOfRangeEnemy(GetComponent<Unit>()),
                        new OutOfRangeEnemy(GetComponent<Unit>()),
                    }),
                }),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckLifeUnder50(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckHealer(GetComponent<Unit>()),
                        new GoHealer(GetComponent<Unit>()),
                    }),
                    new Selector(new List<NodeBT>{
                        new Sequence(new List<NodeBT>{
                            new CheckRangeEnemy(GetComponent<Unit>()),
                            new RangeEnemy(GetComponent<Unit>()),
                        }),
                        new Sequence(new List<NodeBT>
                        {
                            new CheckOutOfRangeEnemy(GetComponent<Unit>()),
                            new OutOfRangeEnemy(GetComponent<Unit>()),
                        }),
                    }),
                }),
            }),
        });
        return root;
    }
}