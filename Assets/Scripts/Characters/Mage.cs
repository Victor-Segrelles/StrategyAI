using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{

    public GameObject firestormVFX;
    public GameObject arcanemissileVFX;
    private GameObject currentFirestormVFX;
    private GameObject currentArcaneMissile1VFX;
    private GameObject currentArcaneMissile2VFX;
    private GameObject currentArcaneMissile3VFX;

    public GameObject elementalPrefab;

    const int firestormDamage = 10;
    const int arcaneMissileDamage = 8;

    // ACTION 1 - FIRESTORM
    public override void PerformAction1()
    {
        ResetSelected();
        Debug.Log("Waiting for target position to be selected.");
        StartCoroutine(WaitForGroundTargetSelection());
        StartCoroutine(WaitForFirestormTargetSelection());
    }

    private IEnumerator WaitForFirestormTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Firestorm(selectedGroundPosition);
    }

    public void Firestorm(Transform target)
    {
        Debug.Log("Mage casts a firestorm.");
        currentFirestormVFX = Instantiate(firestormVFX, target.position, Quaternion.identity);
        currentFirestormVFX.SetActive(true);
        StartCoroutine(PlayFirestormVFX(target));
    }

    private IEnumerator PlayFirestormVFX(Transform target)
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(currentFirestormVFX);
        // TODO: Find enemies en area and damage them
    }

    // ACTION 2 - SUMMON ELEMENTAL
    public override void PerformAction2()
    {
        ResetSelected();
        Debug.Log("Waiting for point on ground to be selected.");
        StartCoroutine(WaitForGroundTargetSelection());
    }

    public void SummonElemental()
    {
        Debug.Log("Mage summons an elemental.");
        Instantiate(elementalPrefab, transform, selectedGroundPosition);

    }

    // ACTION 3 - ARCANE MISSILES
    public override void PerformAction3()
    {
        ResetSelected();
        Debug.Log("Waiting for target characters to be selected.");
        StartCoroutine(WaitForThreeEnemiesTargetSelection());
        StartCoroutine(WaitForArcaneMissilesTargetSelection());
    }

    private IEnumerator WaitForArcaneMissilesTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        ArcaneMissiles(selectedCharacters[0], selectedCharacters[1], selectedCharacters[2]);
    }

    public void ArcaneMissiles(Character target1, Character target2, Character target3)
    {
        currentArcaneMissile1VFX = Instantiate(arcanemissileVFX, target1.transform.position, Quaternion.identity);
        currentArcaneMissile1VFX.SetActive(true);

        currentArcaneMissile2VFX = Instantiate(arcanemissileVFX, target2.transform.position, Quaternion.identity);
        currentArcaneMissile2VFX.SetActive(true);

        currentArcaneMissile3VFX = Instantiate(arcanemissileVFX, target3.transform.position, Quaternion.identity);
        currentArcaneMissile3VFX.SetActive(true);

        StartCoroutine(PlayArcaneMissileVFXAndHit(target1, target2, target3));
        Debug.Log("Mage casts arcane missiles.");
    }

    private IEnumerator PlayArcaneMissileVFXAndHit(Character target1, Character target2, Character target3)
    {
        float elapsedTime = 0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            //Vector3 target1Position = target1.transform.position;
            currentArcaneMissile1VFX.transform.position = Vector3.Lerp(transform.position, target1.transform.position, elapsedTime / duration);
            //Vector3 directionToTarget1 = (target1Position - currentArcaneMissile1VFX.transform.position).normalized;
            //currentArcaneMissile1VFX.transform.rotation = Quaternion.LookRotation(directionToTarget1);

            //Vector3 target2Position = target2.transform.position;
            currentArcaneMissile2VFX.transform.position = Vector3.Lerp(transform.position, target2.transform.position, elapsedTime / duration);
            //Vector3 directionToTarget2 = (target2Position - currentArcaneMissile2VFX.transform.position).normalized;
            //currentArcaneMissile2VFX.transform.rotation = Quaternion.LookRotation(directionToTarget2);

            //Vector3 target3Position = target3.transform.position;
            currentArcaneMissile3VFX.transform.position = Vector3.Lerp(transform.position, target3.transform.position, elapsedTime / duration);
            //Vector3 directionToTarget3 = (target3Position - currentArcaneMissile3VFX.transform.position).normalized;
            //currentArcaneMissile3VFX.transform.rotation = Quaternion.LookRotation(directionToTarget3);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currentArcaneMissile1VFX);
        Destroy(currentArcaneMissile2VFX);
        Destroy(currentArcaneMissile3VFX);

        target1.ReceiveDamage(arcaneMissileDamage);
        target2.ReceiveDamage(arcaneMissileDamage);
        target3.ReceiveDamage(arcaneMissileDamage);
        // TODO: if missile arcanes can hit same character might overkill and crash game
    }
}
