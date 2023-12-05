using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private PlayerCamera camera;
    public Character activeCharacter;
    public Character focusedCharacter;
    public List<Character> selectedCharacters = new List<Character>();
    public Transform selectedGroundPosition;

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

    /// <summary>
    /// Method <c>Move</c> waits for the player to click a valid target position or target character and moves active character accordinly.
    /// </summary>
    public void Move()
    {
        selectedCharacters.Clear();
        selectedGroundPosition = null;
        Debug.Log("Waiting for either target position or target character to be selected.");

        StartCoroutine(WaitForSelection());
    }

    private IEnumerator WaitForSelection()
    {
        while (selectedCharacters.Count == 0 && selectedGroundPosition == null)
        {
            yield return null;
        }

        if (selectedCharacters.Count > 0)
        {
            activeCharacter.Move(selectedCharacters[0].transform);
        }
        else if (selectedGroundPosition != null)
        {
            activeCharacter.Move(selectedGroundPosition);
        }
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
