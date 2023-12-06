using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Character
{
    public GameObject smiteVFX;
    public GameObject healVFX;
    public GameObject healingAreaVFX;
    private GameObject currentSmiteVFX;
    private GameObject currentHealFX;
    private GameObject currentHealingAreaVFX;

    const int smiteDamage = 10;
    const int healPower = 10;
    const int healingAreaPower = 5;

    // ACTION 1 - SMITE
    public override void PerformAction1()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForEnemyTargetSelection());
        StartCoroutine(WaitForSmiteTargetSelection());
    }

    private IEnumerator WaitForSmiteTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Smite(selectedCharacters[0]);
    }

    public void Smite(Character target)
    {
        currentSmiteVFX = Instantiate(smiteVFX, target.transform.position, Quaternion.identity);
        currentSmiteVFX.SetActive(true);
        StartCoroutine(PlaySmiteVFX(target));
    }

    private IEnumerator PlaySmiteVFX(Character target)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(currentSmiteVFX);

        target.ReceiveDamage(smiteDamage);
    }

    // ACTION 2 - HEAL
    public override void PerformAction2()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForAllyTargetSelection());
        StartCoroutine(WaitForHealTargetSelection());
    }

    private IEnumerator WaitForHealTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Heal(selectedCharacters[0]);
    }

    public void Heal(Character target)
    {
        currentHealFX = Instantiate(healVFX, target.transform.position, Quaternion.identity);
        currentHealFX.SetActive(true);
        StartCoroutine(PlayHealVFX(target));
    }

    private IEnumerator PlayHealVFX(Character target)
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(currentHealFX);
        target.ReceiveHealing(healPower);
    }

    // ACTION 3 - HEALING AREA

    public override void PerformAction3()
    {
        ResetSelected();
        Debug.Log("Waiting for target position to be selected.");
        StartCoroutine(WaitForGroundTargetSelection());
        StartCoroutine(WaitForhealingAreaVFXTargetSelection());
    }

    private IEnumerator WaitForhealingAreaVFXTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        healingAreaVFX(selectedGroundPosition);
    }

    public void healingAreaVFX(Transform target)
    {
        Debug.Log("Cleric heals area");
        currentHealingAreaVFX

 = Instantiate(healingAreaVFX
, target.transform.position, Quaternion.identity);
        currentHealingAreaVFX

.SetActive(true);
        StartCoroutine(PlayhealingAreaVFX
(target));
    }

    private IEnumerator PlayhealingAreaVFX(Transform target)
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(currentHealingAreaVFX

);
        // TODO: Find allies in area and heal them
    }
}
