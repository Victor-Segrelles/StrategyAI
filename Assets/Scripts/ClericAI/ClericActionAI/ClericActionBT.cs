using System.Collections.Generic;
using BehaviorTree;

public class ClericActionBT : Tree
{
    /*
    CLERIC
    si la vida est� cr�tica se cura a s� mismo: HEAL(self)
    si varios aliados est�n cerca entre ellos y est�n ambos da�ados: HEALING AREA
    si un aliado cercano est� da�ado, hay que decidir cual curar: HEAL(mas da�ado)
    si ningun aliado cercano o todos est�n al m�ximo de vida: lanzar SMITE al que menos vida tenga*/
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