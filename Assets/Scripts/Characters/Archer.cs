using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Archer : Character
{

    public GameObject arrowVFX;
    private GameObject currentArrowVFX1;
    private GameObject currentArrowVFX2;
    public GameObject evadeVFX;
    private GameObject currentEvadeVFX;
    const int singleShotDamage = 20;
    const int twinShotDamage = 15;
    const float evasionChance = 0.6f;
    private int turnsWithEvasion = 0;

    private void Update()
    {
        if (currentEvadeVFX != null)
        {
            currentEvadeVFX.transform.position = transform.position;
        }

        if (currentEvadeVFX != null && turnsWithEvasion == 0)
        {
            Destroy(currentEvadeVFX);
        }
    }
    public override void startTurn(){
        unit.myturn=true;
        if(turnsWithEvasion>0){
            turnsWithEvasion--;
        }
    }

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
        currentArrowVFX1 = Instantiate(arrowVFX, target.transform.position, Quaternion.identity);
        currentArrowVFX1.SetActive(true);
        skillCompleted = true;
        StartCoroutine(PlayArrowVFXAndHit(target));
    }


    private IEnumerator PlayArrowVFXAndHit(Character target)
    {
        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            currentArrowVFX1.transform.position = Vector3.Lerp(transform.position, target.transform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currentArrowVFX1);

        target.ReceiveDamage(singleShotDamage);
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
        currentArrowVFX1 = Instantiate(arrowVFX, target1.transform.position, Quaternion.identity);
        currentArrowVFX2 = Instantiate(arrowVFX, target2.transform.position, Quaternion.identity);
        currentArrowVFX1.SetActive(true);
        currentArrowVFX2.SetActive(true);
        StartCoroutine(PlayTwinArrowVFXAndHit(target1, target2));
        skillCompleted = true;
    }


    private IEnumerator PlayTwinArrowVFXAndHit(Character target1, Character target2)
    {
        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            currentArrowVFX1.transform.position = Vector3.Lerp(transform.position, target1.transform.position, elapsedTime / duration);
            currentArrowVFX2.transform.position = Vector3.Lerp(transform.position, target2.transform.position, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currentArrowVFX1);
        Destroy(currentArrowVFX2);

        if(target1 != null) {
            target1.ReceiveDamage(twinShotDamage);
        }
        if(target2 != null) {
            target2.ReceiveDamage(twinShotDamage);
        } 
    }

    // ACTION 3 - EVADE
    public override void PerformAction3()
    {
        Evade();
        skillCompleted = true;
    }

    public void Evade()
    {
        turnsWithEvasion = 2;
        if (currentEvadeVFX == null)
        {
            currentEvadeVFX = Instantiate(evadeVFX, transform.position, Quaternion.identity);
        }
        Debug.Log("Archer improves their evasion");
    }

    public override void setNames()
    {
        //characterName = "Arquero";
        firstSkill = ("Single Shot", GameMaster.ActionType.oneTarget);
        secondSkill = ("Twin Shot", GameMaster.ActionType.twoTarget);
        thirdSkill = ("Evade", GameMaster.ActionType.selfTarget);
    }

    public override void ReceiveDamage(int damage)
    {
        Debug.Log("My health before the attack: " +  unit.life);
        float randomValue = Random.value;
        if ((randomValue > evasionChance)||turnsWithEvasion==0) // failed evasion
        {
            int newHealth =  unit.life - damage;
            if (newHealth < 1)
            {
                 unit.life = 0;
                Die();
            }
            else
            {
                 unit.life = newHealth;
            }
        }
        Debug.Log("My health after the attack: " +  unit.life);
    }

    // TODO: DELETE AFTER TESTING
    public void TestSkill1()
    {
        SingleShot(selectedCharacters[0]);
    }

    public void TestSkill2()
    {
        TwinShot(selectedCharacters[0], selectedCharacters[1]);

    }

    public void TestSkill3()
    {
        Evade();
    }
}
