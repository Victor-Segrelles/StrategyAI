using System.Collections.Generic;
using BehaviorTree;

public class ClericActionBT : Tree
{
    /*
    CLERIC
    si la vida está crítica se cura a sí mismo: HEAL(self)
    si varios aliados están cerca entre ellos y están ambos dañados: HEALING AREA
    si un aliado cercano está dañado, hay que decidir cual curar: HEAL(mas dañado)
    si ningun aliado cercano o todos están al máximo de vida: lanzar SMITE al que menos vida tenga*/
    protected override NodeBT SetupTree()
    {
        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>
            {
                new CheckLifeCleric(GetComponent<Unit>()),
                new SelfHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckAOEHeal(GetComponent<Unit>()),
                new AOEHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckWhoIHeal(GetComponent<Unit>()),
                new WhoIHeal(),
            }),
            new Sequence(new List<NodeBT>
            {
                new CheckAttack(GetComponent<Unit>()),
                new Attack(),
            }),
        });
        return root;
    }
}