using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPlaceHolder : MonoBehaviour
{
    
    public float advanceDistance = 5f;
    public float rotateAngle = 90f; // Ángulo de giro en grados
    public float dashDistance = 10f;
    public float movementSpeed = 5f;

    //Nombres
    public string characterName;
    public string firstSkill;
    public string secondSkill;
    public string thirdSkill;

    //Importantes
    private bool isMoving = false;
    private bool movementCompleted = false;

    //Lucas
    public bool isPlayerControlled;

    public List<CharacterPlaceHolder> selectedCharacters = new List<CharacterPlaceHolder>();
    public Transform selectedGroundPosition;
    public bool selectionFinished = false;
    const int MaxHealth = 100;

    public int health = MaxHealth;

    GameManager gm;
    private Renderer rend;
    Color highlightedColor = Color.green;
    Color actualColor;

    public Unit parent;

    void Start()
    {
        rend = GetComponent<Renderer>();
        actualColor = rend.material.color;
        gm = FindObjectOfType<GameManager>();
    }

    //Esta función comprueba si es enemigo o aliado
    public void checkStatus()
    {
        if (isPlayerControlled)
        {
            // show control interface
        }
    }


    public void Move()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            print(characterName + " has moved");
            isMoving = false;
            movementCompleted = true;
        }
    }

    public void Rotate()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            print(characterName + " has rotated");
            isMoving = false;
            movementCompleted = true;
        }
    }

    public void Dash()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            print(characterName + " has dashed");
            isMoving = false;
            movementCompleted = true;
        }
    }

    public void Slide()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            print(characterName + " has slided");
            isMoving = false;
            movementCompleted = true;
        }
    }

    protected IEnumerator WaitForMoveTargetSelection()
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

    protected IEnumerator WaitForEnemyTargetSelection() // TODO: fix missing enemy confirmation functionality
    {
        while (selectedCharacters.Count < 1)
        {
            yield return null;
        }
        selectionFinished = true;
    }

    protected IEnumerator WaitForTwoEnemiesTargetSelection() // TODO: fix missing enemy confirmation functionality
    {
        while (selectedCharacters.Count < 2)
        {
            yield return null;
        }
        selectionFinished = true;
    }

    protected IEnumerator WaitForThreeEnemiesTargetSelection() // TODO: fix missing enemy confirmation functionality
    {
        while (selectedCharacters.Count < 3)
        {
            yield return null;
        }
        selectionFinished = true;
    }

    protected IEnumerator WaitForAllyTargetSelection() // TODO: fix missing ally confirmation functionality
    {
        while (selectedCharacters.Count < 1)
        {
            yield return null;
        }
        selectionFinished = true;
    }

    protected IEnumerator WaitForGroundTargetSelection()
    {
        while (selectedGroundPosition == null)
        {
            yield return null;
        }
        selectionFinished = true;

    }
    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsMovementCompleted()
    {
        return movementCompleted;
    }

    public void ResetMovementStatus()
    {
        isMoving = false;
        movementCompleted = false;
    }

    public virtual void PerformAction1()
    {
        Rotate();
    }

    public virtual void PerformAction2()
    {
        Dash();
    }

    public virtual void PerformAction3()
    {
        Slide();
    }

    public void Highlight()
    {
        rend.material.color = highlightedColor;
    }

    private void OnMouseEnter()
    {
        Highlight();
    }

    private void OnMouseExit()
    {
        Reset();
    }

    public void Reset()
    {
        rend.material.color = actualColor;
    }


    private void OnMouseDown()
    {
        
        gm.GetCurrentCharacter().selectedCharacters.Add(this);
    }

    public void ReceiveDamage(int damage)
    {
        int newHealth = health - damage;
        if (newHealth < 1)
        {
            health = 0;
            Die();
        }
        else
        {
            health = newHealth;
        }
    }

    public void ReceiveHealing(int heal)
    {
        int newHealth = health + heal;
        if (newHealth > MaxHealth)
        {
            health = MaxHealth;
        }
        else
        {
            health = newHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Character died.");
    }
}