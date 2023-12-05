using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Character
{
    public override void PerformAction1()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForEnemyTargetSelection());
        StartCoroutine(WaitForSmiteTargetSelection());
    }

    public override void PerformAction2()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForAllyTargetSelection());
        StartCoroutine(WaitForHealTargetSelection());
    }

    public override void PerformAction3()
    {
        ResetSelected();
        Debug.Log("Waiting for either target position to be selected.");
        StartCoroutine(WaitForGroundTargetSelection());
        StartCoroutine(WaitForHealingAreaTargetSelection());
    }

    private IEnumerator WaitForSmiteTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Smite(selectedCharacters[0]);
    }

    private IEnumerator WaitForHealTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Heal(selectedCharacters[0]);
    }

    private IEnumerator WaitForHealingAreaTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        HealingArea(selectedGroundPosition);
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
