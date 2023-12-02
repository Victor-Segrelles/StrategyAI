using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private PlayerCamera camera;
    public Character activeCharacter;
    public Character focusedCharacter;
    public Character selectedCharacter;
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
    }

    /// <summary>
    /// Method <c>Move</c> waits for the player to click a valid target position or target character and moves active character accordinly.
    /// </summary>
    public void Move()
    {
        selectedCharacter = null;
        selectedGroundPosition = null;
        Debug.Log("Waiting for either target position or target character to be selected.");

        StartCoroutine(WaitForSelection());
    }

    private IEnumerator WaitForSelection()
    {
        while (selectedCharacter == null && selectedGroundPosition == null)
        {
            yield return null;
        }

        if (selectedCharacter != null)
        {
            activeCharacter.Move(selectedCharacter.transform);
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
}
