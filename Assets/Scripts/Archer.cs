using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    public override void PerformAction1()
    {

    }

    public override void PerformAction2()
    {
        
    }

    public override void PerformAction3()
    {

    }
    
    public void SingleShot(Character target)
    {
        Debug.Log("Archer shoots 1 arrow");
    }

    public void TwinShot(Character target1, Character target2)
    {
        Debug.Log("Archer shoots 2 arrows");
    }

    public void Evade()
    {
        Debug.Log("Archer improves their evasion");
    }
}
