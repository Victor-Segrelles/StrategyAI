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

    public List<Character> selectedCharacters = new List<Character>();
    public Transform selectedGroundPosition;
    public bool selectionFinished = false;
    const int MaxHealth = 100;

    public int health = MaxHealth;

    GameManager gm;
    private Renderer rend;
    Color highlightedColor = Color.green;


    void Start()
    {
        rend = GetComponent<Renderer>();
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
}