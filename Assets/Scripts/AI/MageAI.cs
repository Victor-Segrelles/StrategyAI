using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI : Mage
{
    private GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseTarget()
    {
        float attackDistance = 20f;
        List<Character> possibleEnemies = new List<Character>();
        Character closestOutsideRange = null;
        foreach (Character enemy in gm.allies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < attackDistance)
            {
                possibleEnemies.Add(enemy);
            }
            else
            {
                if (closestOutsideRange == null)
                {
                    closestOutsideRange = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(closestOutsideRange.transform.position, enemy.transform.position))
                    {
                        closestOutsideRange = enemy;
                    }
                }
            }
        }
        if (possibleEnemies.Count > 0)
        {
            
        }
    }
}
