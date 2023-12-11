using System.Collections.Generic;
using BehaviorTree;

public class WarriorBT : Tree
{
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeOver50Warrior(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckRangeEnemyWarrior(GetComponent<Unit>()),
                        new RangeEnemyWarrior(GetComponent<Unit>()),
                    }),
                    new Sequence(new List<NodeBT>
                    {
                        new CheckOutOfRangeEnemyWarrior(GetComponent<Unit>()),
                        new OutOfRangeEnemyWarrior(GetComponent<Unit>()),
                    }),
                }),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckLifeUnder50Warrior(GetComponent<Unit>()),
                new Selector(new List<NodeBT>{
                    new Sequence(new List<NodeBT>{
                        new CheckHealerWarrior(GetComponent<Unit>()),
                        new GoHealerWarrior(GetComponent<Unit>()),
                    }),
                    new Selector(new List<NodeBT>{
                        new Sequence(new List<NodeBT>{
                            new CheckRangeEnemy(GetComponent<Unit>()),
                            new RangeEnemy(GetComponent<Unit>()),
                        }),
                        new Sequence(new List<NodeBT>
                        {
                            new CheckOutOfRangeEnemyWarrior(GetComponent<Unit>()),
                            new OutOfRangeEnemyWarrior(GetComponent<Unit>()),
                        }),
                    }),
                }),
            }),
        });
        return root;
    }
}