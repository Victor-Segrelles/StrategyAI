using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Character
{
    public GameObject smiteVFX;
    public GameObject healVFX;
    public GameObject HealingAreaVFX;
    const int smiteDamage = 10;
    const int healPower = 10;
    const int healingAreaPower = 5;

    private GameObject currentSmiteVFX;
    private GameObject currentHealFX;
    private GameObject currentHealingAreaVFX;


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
        currentSmiteVFX = Instantiate(smiteVFX, target.transform.position, Quaternion.identity);
        currentSmiteVFX.SetActive(true);
        StartCoroutine(PlaySmiteVFX(target));
    }

    public void Heal(Character target)
    {
        currentHealFX = Instantiate(healVFX, target.transform.position, Quaternion.identity);
        currentHealFX.SetActive(true);
        StartCoroutine(PlayHealVFX(target));
    }

    public void HealingArea(Transform target)
    {
        Debug.Log("Cleric heals area");
    }

    private IEnumerator PlaySmiteVFX(Character target)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(currentSmiteVFX);

        target.ReceiveDamage(smiteDamage);
        Debug.Log("Target health: " + target.health);
    }

    private IEnumerator PlayHealVFX(Character target)
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(currentHealFX);
        target.ReceiveHealing(healPower);
        Debug.Log("Target health: " + target.health);
    }
}
