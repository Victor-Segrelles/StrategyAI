using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Nombres
    public string characterName;
    public string firstSkill;
    public string secondSkill;
    public string thirdSkill;


    private bool skillCompleted = false;

    //Comprobante del tipo de personaje
    public bool isPlayerControlled;

    //Variables de selección
    public List<Character> selectedCharacters = new List<Character>();
    public Transform selectedGroundPosition ;
    public bool selectionFinished = false;

    //Control de salud
    const int MaxHealth = 100;
    public int health = MaxHealth;

    //Importar otros scripts
    public GameMaster gm;
    public Unit parent;

    //Código gráfico para resaltar color
    public Renderer rend;
    Color highlightedColor = Color.green;
    Color actualColor;


    //Comprobantes del movimiento
    private bool isMoving = false;
    private bool movementCompleted = false;


    //Comprobantes de la accion
    private bool isCastingSkill = false;

    private void Awake()
    {
        setNames();
    }
    void Start()
    {
        
        rend = GetComponent<Renderer>();
        actualColor = rend.material.color;
        gm = FindObjectOfType<GameMaster>();
        parent = GetComponent<Unit>();
        selectedGroundPosition = this.transform;

    }

    //Código de movimiento
    public void ResetMovementStatus()
    {
        //selectedGroundPosition = null;
        isMoving = false;
        movementCompleted = false;

    }
    
    public void Move()
    {
        
        parent.ChangeTarget(selectedGroundPosition);
        print("Jejeje, me moví a " + selectedGroundPosition);
        

        StartCoroutine(CheckMovement());

    }

    public void WarnMove()
    {
        ResetMovementStatus();
        print("Is going to move");
    }

    IEnumerator CheckMovement()
    {
        isMoving = true;

        while (Mathf.Abs(Vector3.Distance(transform.position, selectedGroundPosition.position)) >= 2.5f)
        {
            
            print("Me estoy moviendo todavia churrita");
            yield return null;
        }

        isMoving = false;
        movementCompleted = true;
        print("Me terminé de mover");

    }

    ////////////////////////////////////////////////////////////

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

    //Setear la variable selectedGroundPosition
    public void selectGroundPosition(Transform pos)
    {
        selectedGroundPosition = pos;
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


    //
    public void ResetSelected()
    {
        selectedCharacters.Clear();
        selectedGroundPosition = null;
        selectionFinished = false;
    }


    //Actions
    public bool IsCastingsSkill()
    {
        return isCastingSkill;
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
        gm.charactersList.Remove(this);
        Destroy(this.gameObject);
    }









    ////////////////////////////////////////// Pruebas de movimiento /////////////////////////////////////////////////////////

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
}
