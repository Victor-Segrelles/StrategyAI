using System.Collections.Generic;
using BehaviorTree;

public class ClericBT : Tree
{
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeOver50Cleric(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckRangeEnemyCleric(GetComponent<Unit>()),
                        new RangeEnemyCleric(GetComponent<Unit>()),
                    }),
                    new Sequence(new List<NodeBT>
                    {
                        new CheckOutOfRangeEnemyCleric(GetComponent<Unit>()),
                        new OutOfRangeEnemyCleric(GetComponent<Unit>()),
                    }),
                }),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckLifeUnder50Cleric(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckHealerCleric(GetComponent<Unit>()),
                        new GoHealerCleric(GetComponent<Unit>()),
                    }),
                    new Selector(new List<NodeBT>{
                        new Sequence(new List<NodeBT>{
                            new CheckRangeEnemyCleric(GetComponent<Unit>()),
                            new RangeEnemyCleric(GetComponent<Unit>()),
                        }),
                        new Sequence(new List<NodeBT>
                        {
                            new CheckOutOfRangeEnemyCleric(GetComponent<Unit>()),
                            new OutOfRangeEnemyCleric(GetComponent<Unit>()),
                        }),
                    }),
                }),
            }),
        });
        return root;
    }
}