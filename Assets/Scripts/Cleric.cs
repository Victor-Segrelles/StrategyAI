using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Character
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
    
    public void Smite(Character target)
    {
        Debug.Log("Cleric smites 1 target");
    }

    public void Heal(Character target)
    {
        Debug.Log("Cleric heals 1 target");
    }

    public void HealingArea(Transform target)
    {
        Debug.Log("Cleric heals area");
    }
}
