using System.Collections.Generic;
using BehaviorTree;

public class ArcherBT : Tree
{
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeOver50Archer(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckRangeEnemyArcher(GetComponent<Unit>()),
                        new RangeEnemyArcher(GetComponent<Unit>()),
                    }),
                    new Sequence(new List<NodeBT>
                    {
                        new CheckOutOfRangeEnemyArcher(GetComponent<Unit>()),
                        new OutOfRangeEnemyArcher(GetComponent<Unit>()),
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