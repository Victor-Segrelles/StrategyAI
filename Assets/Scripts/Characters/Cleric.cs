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
    const int healingAreaRadius=5;

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
        skillCompleted = true;
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
        skillCompleted = true;
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
        //print(selectedGroundPosition.position);
        ResetSelected();
        //print(selectedGroundPosition.position);
        Debug.Log("Waiting for target position to be selected.");
        StartCoroutine(WaitForGroundTargetSelection());
        StartCoroutine(WaitForhealingAreaTargetSelection());
    }

    private IEnumerator WaitForhealingAreaTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        healingArea(selectedGroundPosition);
        skillCompleted = true;
    }

    public void healingArea(Transform target)
    {
        Debug.Log("Cleric heals area");
        currentHealingAreaVFX = Instantiate(healingAreaVFX, target.transform.position, Quaternion.identity);
        currentHealingAreaVFX.SetActive(true);
        StartCoroutine(PlayHealingAreaVFX(target));
    }

    private IEnumerator PlayHealingAreaVFX(Transform target)
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(currentHealingAreaVFX);

        if (isPlayerControlled)
        {
            foreach (Character ally in gm.allies)
            {
                if (Vector3.Distance(target.position, ally.transform.position) < healingAreaRadius)
                {
                    ally.ReceiveHealing(healingAreaPower);
                }
            }
        }
        else
        {
            foreach (Character ally in gm.enemies)
            {
                if (Vector3.Distance(target.position, ally.transform.position) < healingAreaRadius)
                {
                    ally.ReceiveHealing(healingAreaPower);
                }
            }
        }
    }


    public override void setNames()
    {

        firstSkill = ("Smite", GameMaster.ActionType.oneTarget);
        secondSkill = ("Heal", GameMaster.ActionType.allieTarget);
        thirdSkill = ("Healing area", GameMaster.ActionType.groundTarget);
    }

    // TODO: DELETE AFTER TESTING
    public void TestSkill1()
    {
        Smite(selectedCharacters[0]);
    }

    public void TestSkill2()
    {
        Heal(selectedCharacters[0]);
    }

    public void TestSkill3()
    {
        healingArea(selectedCharacters[0].transform);
    }
}
