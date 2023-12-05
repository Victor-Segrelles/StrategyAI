using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    public override void PerformAction1()
    {
        //Slash(targetCharacter);
    }

    public override void PerformAction2()
    {
        
    }

    public override void PerformAction3()
    {

    }
    public void Slash(Character target)
    {
        Debug.Log("Warrior slashes target enemy");
    }

    public void Cleave(Transform target)
    {
        Debug.Log("Warrior cleaves target location (melee range)");
    }

    public void Taunt(Character target)
    {
        Debug.Log("Warrior taunts target enemy");
    }
}
