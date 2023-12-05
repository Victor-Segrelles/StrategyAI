using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isPlayerControlled;

	public Unit parent;
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

    public void Move(Transform target) // should be limited by movementAmountLeft
    {
        parent.ChangeTarget(target);
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
        gm.selectedCharacters.Add(this);
    }

    public void Highlight()
    {
        rend.material.color = highlightedColor;
    }

    public void Reset()
    {
        rend.material.color = Color.white;
    }
}
