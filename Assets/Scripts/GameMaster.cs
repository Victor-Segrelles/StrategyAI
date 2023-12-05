using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private PlayerCamera camera;
    public Character activeCharacter;
    public Character focusedCharacter;

    private void Start()
    {
        camera = FindObjectOfType<PlayerCamera>();
    }

    public void FocusCharacter()
    {
        camera.FocusCharacter();
    }

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
        
    }

    public void PerformAction2()
    {
        
    }

    public void PerformAction3()
    {
        
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
