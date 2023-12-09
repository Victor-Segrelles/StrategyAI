using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPlaceHolder : CharacterPlaceHolder
{
    // ACTION 1 - SINGLE SHOT
    public override void PerformAction1()
    {
        ResetSelected();
        SingleShot(this);
    }

    public void SingleShot(CharacterPlaceHolder target)
    {
        Debug.Log("Archer shoots 1 arrow");
    }

    // ACTION 2 - TWIN SHOT
    public override void PerformAction2()
    {
        ResetSelected();
        TwinShot(this, this);
    }

    public void TwinShot(CharacterPlaceHolder target1, CharacterPlaceHolder target2)
    {
        Debug.Log("Archer shoots 2 arrows");
    }

    // ACTION 3 - EVADE
    public override void PerformAction3()
    {
        ResetSelected();
        Evade();
    }

    public void Evade()
    {
        Debug.Log("Archer improves their evasion");
    }

    public override void setNames()
    {
        //characterName = "Arquero";
        firstSkill = "Single Shot";
        secondSkill = "Twin Shot";
        thirdSkill = "Evade";
    }
}
