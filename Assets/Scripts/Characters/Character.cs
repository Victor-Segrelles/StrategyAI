using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Variables

    //Nombres
    public string characterName;
    public string firstSkill;
    public string secondSkill;
    public string thirdSkill;


    private bool skillCompleted = false;

    //Comprobante del tipo de personaje
    public bool isPlayerControlled;

    //Variables de selecci�n
    public List<Character> selectedCharacters = new List<Character>();
    public Transform selectedGroundPosition;
    public bool selectionFinished = false;

    //Control de salud
    const int MaxHealth = 100;
    public int health = MaxHealth;

    //Importar otros scripts
    private GameMaster gm;
    private Unit unit;

    //C�digo gr�fico para resaltar color
    public Renderer rend;
    Color highlightedColor = Color.green;
    Color actualColor;


    //Comprobantes del movimiento
    private bool isMoving = false;
    private bool movementCompleted = false;


    //Comprobantes de la accion
    private bool isCastingSkill = false;

    #endregion

    #region M�todos

    #region Inicializadores
    private void Awake()
    {
        setNames();


    }
    void Start()
    {
        
        rend = GetComponent<Renderer>();
        actualColor = rend.material.color;
        gm = FindObjectOfType<GameMaster>();
        unit = GetComponent<Unit>();
        //selectedGroundPosition = this.transform;


    }

    #endregion

    #region Movement
    //C�digo de movimiento
    public void ResetMovementStatus()
    {
        //selectedGroundPosition = null;
        isMoving = false;
        movementCompleted = false;

    }
    
    public void Move()
    {
        //
        ////// Preguntar si el Unit puede ir all�
        //
        unit.ChangeTarget(selectedGroundPosition);
        print("Jejeje, me mov� a " + selectedGroundPosition);
        

        StartCoroutine(CheckMovement());

    }

    public void WarnMove()
    {
        ResetMovementStatus();
        print("Is going to move");
    }

    //Comprueba si el personaje se est� moviendo o ejecutando la acci�n
    public bool IsMoving()
    {
        return isMoving;
    }

    //Comprueba si se ha terminado el movimiento
    public bool IsMovementCompleted()
    {
        return movementCompleted;
    }

    IEnumerator CheckMovement()
    {
        isMoving = true;

        while (Mathf.Abs(Vector3.Distance(transform.position, selectedGroundPosition.position)) >= 4f)
        {
            //print(Mathf.Abs(Vector3.Distance(transform.position, selectedGroundPosition.position)));
            print("Me estoy moviendo todavia churrita");
            yield return null;
        }

        isMoving = false;
        movementCompleted = true;
        //print("Me termin� de mover");

    }

    #endregion


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
        this.firstSkill = "1� skill";
        this.secondSkill = "2� skill";
        this.thirdSkill = "3� skill";
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

    #region Control de da�o
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
        gm.auxTransform.Remove(selectedGroundPosition);
        Destroy(selectedGroundPosition.gameObject);
        Destroy(this.gameObject);
    }

    #endregion

    #endregion






}
