using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    // ACTION 1 - SLASH
    public override void PerformAction1()
    {
        ResetSelected();
    }

    public void Slash(Character target)
    {
        Debug.Log("Warrior slashes target enemy");
    }

    // ACTION 2 - CLEAVE
    public override void PerformAction2()
    {
        ResetSelected();
    }

    public void Cleave(Transform target)
    {
        Debug.Log("Warrior cleaves target location (melee range)");
    }

    // ACTION 3 - TAUNT
    public override void PerformAction3()
    {
        ResetSelected();
    }

    public void Taunt(Character target)
    {
        Debug.Log("Warrior taunts target enemy");
    }
}
