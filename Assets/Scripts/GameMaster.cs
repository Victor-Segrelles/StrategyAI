using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Character activeCharacter;

    public void StarTurn()
    {
        activeCharacter.StartTurn();
        if (activeCharacter.isPlayerControlled)
        {
            // show interface according to character type: mage | warrior | archer | cleric
        }
    }

    public void Move()
    {
        activeCharacter.Move();
    }

    public void PerformAction1()
    {
        activeCharacter.PerformAction1();
    }

    public void PerformAction2()
    {
        activeCharacter.PerformAction2();

    }

    public void PerformAction3()
    {
        activeCharacter.PerformAction3();
    }

    public void MageTurn() // TODO: DELETE
    {
        Mage mage = FindObjectOfType<Mage>();
        activeCharacter = mage;
    }

    public void WarriorTurn() // TODO: DELETE
    {
        Warrior warrior = FindObjectOfType<Warrior>();
        activeCharacter = warrior;
    }
}
