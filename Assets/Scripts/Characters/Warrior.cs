using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Warrior : Character
{
    public GameObject slashVFX;
    public GameObject cleaveVFX;
    private GameObject currentSlashVFX;
    private GameObject currentCleaveVFX;

    int slashDamage = 25;
    int cleaveDamage = 20;
    int cleaveRadius = 5;

    // ACTION 1 - SLASH
    public override void PerformAction1()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForEnemyTargetSelection());
        StartCoroutine(WaitForSlashTargetSelection());
    }

    private IEnumerator WaitForSlashTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Slash(selectedCharacters[0]);
        skillCompleted = true;
    }

    public void Slash(Character target)
    {
        currentSlashVFX = Instantiate(slashVFX, target.transform.position, Quaternion.identity);
        currentSlashVFX.SetActive(true);
        StartCoroutine(PlaySlashVFX(target));
        Debug.Log("Warrior slashes target enemy");
    }

    private IEnumerator PlaySlashVFX(Character target)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(currentSlashVFX);

        target.ReceiveDamage(slashDamage);
    }

    // ACTION 2 - CLEAVE
    public override void PerformAction2()
    {
        ResetSelected();
        Cleave();
        skillCompleted = true;
    }

    public void Cleave()
    {
        Debug.Log("Warrior cleaves target location (melee range)");
        currentCleaveVFX = Instantiate(cleaveVFX, transform.position, Quaternion.identity);
        currentCleaveVFX.SetActive(true);
        StartCoroutine(PlayCleaveVFX(this.transform));
    }

    private IEnumerator PlayCleaveVFX(Transform target)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(currentCleaveVFX);

        if (isPlayerControlled)
        {
            foreach (Character enemy in gm.enemies)
            {
                if (Vector3.Distance(target.position, enemy.transform.position) < cleaveRadius)
                {
                    enemy.ReceiveDamage(cleaveDamage);
                }
            }
        }
        else
        {
            foreach (Character enemy in gm.allies)
            {
                if (Vector3.Distance(target.position, enemy.transform.position) < cleaveRadius)
                {
                    enemy.ReceiveDamage(cleaveDamage);
                }
            }
        }
}

    // ACTION 3 - STUN
    public override void PerformAction3()
    {
        ResetSelected();
        Debug.Log("Waiting for target character to be selected.");
        StartCoroutine(WaitForEnemyTargetSelection());
        StartCoroutine(WaitForStunTargetSelection());
    }

    private IEnumerator WaitForStunTargetSelection()
    {
        while (!selectionFinished)
        {
            yield return null;
        }
        Stun(selectedCharacters[0]);
        skillCompleted = true;
    }

    public void Stun(Character target)
    {
        Debug.Log("Warrior stuns target enemy");
        target.GetStunned();
    }

    public override void setNames()
    {
        //characterName = "Guerrero";
        firstSkill = ("Slash", GameMaster.ActionType.oneTarget);
        secondSkill = ("Cleave", GameMaster.ActionType.selfTarget);
        thirdSkill = ("Stun", GameMaster.ActionType.oneTarget);
    }

    // TODO: DELETE AFTER TESTING
    public void TestSkill1()
    {
        Slash(selectedCharacters[0]);
    }

    public void TestSkill2()
    {
        Cleave();
    }

    public void TestSkill3()
    {
        Stun(selectedCharacters[0]);
    }
}
