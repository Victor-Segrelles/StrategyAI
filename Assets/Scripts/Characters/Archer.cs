using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Archer : Character
{

    public GameObject arrowVFX;
    private GameObject currentArrowVFX;

    public GameObject TwinArrowVFX1;
    private GameObject currentTwinArrowVFX1;
    public GameObject TwinArrowVFX2;
    private GameObject currentTwinArrowVFX2;

    const int arrowDamage = 10;
    public override void PerformAction1()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForEnemyTargetSelection());
        StartCoroutine(WaitForSingleShotTargetSelection());
    }

    private IEnumerator WaitForSingleShotTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        SingleShot(selectedCharacters[0]);
    }

    public void SingleShot(Character target)
    {
        Debug.Log("Archer shoots 1 arrow");
        currentArrowVFX = Instantiate(arrowVFX, target.transform.position, Quaternion.identity);
        currentArrowVFX.SetActive(true);
        StartCoroutine(PlayArrowVFXAndHit(target));
    }


    private IEnumerator PlayArrowVFXAndHit(Character target)
    {
        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            //Vector3 target1Position = target1.transform.position;
            currentArrowVFX.transform.position = Vector3.Lerp(transform.position, target.transform.position, elapsedTime / duration);
            //Vector3 directionToTarget1 = (target1Position - currentArcaneMissile1VFX.transform.position).normalized;
            //currentArcaneMissile1VFX.transform.rotation = Quaternion.LookRotation(directionToTarget1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currentArrowVFX);

        target.ReceiveDamage(arrowDamage);
    }

    // ACTION 2 - TWIN SHOT
    public override void PerformAction2()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForTwoEnemiesTargetSelection());
        StartCoroutine(WaitForTwinShotTargetSelection());
    }

    private IEnumerator WaitForTwinShotTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        TwinShot(selectedCharacters[0], selectedCharacters[1]);
    }

    public void TwinShot(Character target1, Character target2)
    {
        Debug.Log("Archer shoots 2 arrows");
        currentTwinArrowVFX1 = Instantiate(TwinArrowVFX1, target1.transform.position, Quaternion.identity);
        currentTwinArrowVFX2 = Instantiate(TwinArrowVFX2, target2.transform.position, Quaternion.identity);
        currentTwinArrowVFX1.SetActive(true);
        currentTwinArrowVFX2.SetActive(true);
        StartCoroutine(PlayTwinArrowVFXAndHit(target1, target2));
    }


    private IEnumerator PlayTwinArrowVFXAndHit(Character target1, Character target2)
    {
        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            //Vector3 target1Position = target1.transform.position;
            currentTwinArrowVFX1.transform.position = Vector3.Lerp(transform.position, target1.transform.position, elapsedTime / duration);
            //Vector3 directionToTarget1 = (target1Position - currentArcaneMissile1VFX.transform.position).normalized;
            //currentArcaneMissile1VFX.transform.rotation = Quaternion.LookRotation(directionToTarget1);
            //Vector3 target1Position = target1.transform.position;
            currentTwinArrowVFX2.transform.position = Vector3.Lerp(transform.position, target2.transform.position, elapsedTime / duration);
            //Vector3 directionToTarget1 = (target1Position - currentArcaneMissile1VFX.transform.position).normalized;
            //currentArcaneMissile1VFX.transform.rotation = Quaternion.LookRotation(directionToTarget1);



            elapsedTime += Time.deltaTime;
            yield return null;
        }

         elapsedTime = 0f;
         duration = 3f;


        Destroy(currentTwinArrowVFX1);
        Destroy(currentTwinArrowVFX2);

        target1.ReceiveDamage(arrowDamage);
        target2.ReceiveDamage(arrowDamage);
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
