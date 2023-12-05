using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isPlayerControlled;

	public Unit parent;

    public List<Character> selectedCharacters = new List<Character>();
    public Transform selectedGroundPosition;
    const int MaxHealth = 100;
    const int MaxMovementAmount = 100;

    int health = MaxHealth;
    int movementAmountLeft = 0;

    GameMaster gm;
    private Renderer rend;
    Color highlightedColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
        gm = FindObjectOfType<GameMaster>();
    }

    public void StartTurn()
    {
        movementAmountLeft = MaxMovementAmount;
        if (isPlayerControlled)
        {
            // show control interface
        }
    }

    public void Move() // should be limited by movementAmountLeft
    {
        ResetSelected();
        Debug.Log("Waiting for either target position or target character to be selected.");
        StartCoroutine(WaitForMoveTargetSelection());
    }

    private IEnumerator WaitForMoveTargetSelection()
    {
        while (selectedCharacters.Count == 0 && selectedGroundPosition == null)
        {
            yield return null;
        }

        if (selectedCharacters.Count > 0)
        {
            parent.ChangeTarget(selectedCharacters[0].transform);
        }
        else if (selectedGroundPosition != null)
        {
            parent.ChangeTarget(selectedGroundPosition);
        }
    }

    public virtual void PerformAction1() {}
    public virtual void PerformAction2() {}
    public virtual void PerformAction3() {}

    private void OnMouseEnter()
    {
        if (this != gm.activeCharacter)
        {
            Highlight();
        }
    }

    private void OnMouseExit() {
        Reset();
    }

    private void OnMouseDown()
    {
        gm.activeCharacter.selectedCharacters.Add(this);
    }

    public void Highlight()
    {
        rend.material.color = highlightedColor;
    }

    public void Reset()
    {
        rend.material.color = Color.white;
    }

    public void ResetSelected()
    {
        selectedCharacters.Clear();
        selectedGroundPosition = null;
    }
}
