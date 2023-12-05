using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    public override void PerformAction1()
    {
        ResetSelected();
    }

    public override void PerformAction2()
    {
        ResetSelected();
    }

    public override void PerformAction3()
    {
        ResetSelected();
    }
    public void Fireball(Transform target)
    {
        Debug.Log("Mage casts a fireball.");
    }

    public void SummonElemental()
    {
        Debug.Log("Mage summons an elemental.");
    }

    public void ArcaneMissiles(Character target1, Character target2, Character target3)
    {
        Debug.Log("Mage casts arcane missiles to up to three different targets.");
    }
}
