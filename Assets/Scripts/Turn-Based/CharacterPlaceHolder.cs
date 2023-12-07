using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;

public class CharacterPlaceHolder : MonoBehaviour
{
    
    //Nombres
    public string characterName;
    public string firstSkill;
    public string secondSkill;
    public string thirdSkill;

    //Comprobantes de que se ha terminado
    private bool isMoving = false;
    private bool movementCompleted = false;

    private bool skillCompleted =false;

    //Comprobante del tipo de personaje
    public bool isPlayerControlled;

    //Variables de selección
    public List<CharacterPlaceHolder> selectedCharacters = new List<CharacterPlaceHolder>();
    public Transform selectedGroundPosition;
    public bool selectionFinished = false;
    
    //Control de salud
    const int MaxHealth = 100;
    public int health = MaxHealth;

    //Importar otros scripts
    private GameManager gm;
    [SerializeField] private Unit parent;

    //Código gráfico para resaltar color
    private Renderer rend;
    Color highlightedColor = Color.green;
    Color actualColor;

    private void Awake()
    {
        setNames();
    }

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

    //////////////////////////////////////////////Codigo de prueba//////////////////////////////////////////////
    public void MoveForward()
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

    //////////////////////////////////////////////Codigo de prueba//////////////////////////////////////////////



    public void Move() // should be limited by movementAmountLeft
    {
        ResetSelected();
        Debug.Log("Waiting for either target position or target character to be selected.");
        StartCoroutine(WaitForMoveTargetSelection());
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

    //Comprueba si el personaje se está moviendo o ejecutando la acción
    public bool IsMoving()
    {
        return isMoving;
    }

    //Comprueba si se ha terminado el movimiento
    public bool IsMovementCompleted()
    {
        return movementCompleted;
    }

    //Resetea las variables de turno al empezar el turno
    public void ResetMovementStatus()
    {
        isMoving = false;
        movementCompleted = false;
    }

    //
    public void ResetSelected()
    {
        selectedCharacters.Clear();
        selectedGroundPosition = null;
        selectionFinished = false;
    }

    public virtual void PerformAction1()
    {
        
    }

    public virtual void PerformAction2()
    {
        
    }

    public virtual void PerformAction3()
    {
        
    }

    public virtual void setNames()
    {
        //this.characterName = "Character";
        this.firstSkill = "1º skill";
        this.secondSkill = "2º skill";
        this.thirdSkill = "3º skill";
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