using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class Character : MonoBehaviour
{
    #region Variables

    public GameObject stunVFX;
    private GameObject currentStunVFX;

    //Nombres
    public string characterName;
    public (string, ActionType) firstSkill;
    public (string, ActionType) secondSkill;
    public (string, ActionType) thirdSkill;

    //Control de skills
    public bool skillCompleted = false;
    //private bool evaded;

    //Comprobante del tipo de personaje
    public bool isPlayerControlled;

    //Variables de selecci�n
    public List<Character> selectedCharacters = new List<Character>();
    public Transform selectedGroundPosition;
    public bool selectionFinished = false;

    public Transform selectedMovementPosition;

    //Control de salud
    const int MaxHealth = 100;

    public bool isStunned = false;

    //Importar otros scripts
    protected GameMaster gm;
    protected Unit unit;


    //Comprobantes del movimiento
    private bool isMoving = false;
    public bool movementCompleted = false;

    public float movementDistance = 15f;


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
        
        
        //rend = GetComponent<Renderer>();
        //actualColor = rend.material.color;
        gm = FindObjectOfType<GameMaster>();
        unit = GetComponent<Unit>();
        unit.life=MaxHealth;
        //selectedGroundPosition = this.transform;


    }

    private void Update()
    {
        if (currentStunVFX != null)
        {
            currentStunVFX.transform.position = transform.position;
        }

        if (!isStunned && currentStunVFX != null)
        {
            Destroy(currentStunVFX);
        }
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
        print(Mathf.Abs(Vector3.Distance(transform.position, selectedMovementPosition.position)));
        if(Mathf.Abs(Vector3.Distance(transform.position, selectedMovementPosition.position)) <= movementDistance)
        {
            unit.ChangeTarget(selectedMovementPosition);
            StartCoroutine(CheckImpasse());
            
        }
        else
        {
            print("Prueba otro sitio, que ese est� muy lejos");
        }
       
    }

    public void WarnMove()
    {
        //ResetMovementStatus();
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

        while (Mathf.Abs(Vector3.Distance(transform.position, selectedMovementPosition.position)) >= 4f)
        {
            //print(Mathf.Abs(Vector3.Distance(transform.position, selectedGroundPosition.position)));
            print("Me estoy moviendo todavia churrita");
            yield return null;
        }

        isMoving = false;
        movementCompleted = true;
        //print("Me termin� de mover");

    }

    IEnumerator CheckImpasse()
    {
        Vector3 posicionInicial = transform.position;

        yield return new WaitForSeconds(0.25f);


        if (Vector3.Distance(posicionInicial, transform.position) > 0.01f)
        {
            // Si se ha movido, imprimir un mensaje y salir de la corrutina
            Debug.Log("El objeto se ha movido durante la espera. Saliendo de la corrutina.");
            StartCoroutine(CheckMovement());
            yield break;
        }
        else
        {
            // Si no se ha movido, imprimir un mensaje
            Debug.Log("Prueba otro sitio, que eso es terreno inaccesible");
        }

    }

    public void SelectMovementPosition(Transform pos)
    {
        selectedMovementPosition = pos;
    }


    #endregion

    #region Target
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
        print("entered the ground selection");

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

    public void ResetSelected()
    {
        selectedCharacters.Clear();
        selectedGroundPosition = null;
        selectionFinished = false;
    }

    public void ResetTargetStatus()
    {
        //selectedGroundPosition = null;
        isCastingSkill = false;
        skillCompleted = false;

    }
    #endregion


    #region Skills
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
    public virtual void endTurn(){
        unit.endTurn();
    }
    public virtual void startTurn(){
        unit.myturn=true;
    }

    public bool SkillCompleted()
    {
        return skillCompleted;
    }

    #endregion

    public virtual void setNames()
    {
        //this.characterName = "Character";
        this.firstSkill = ("1� skill", ActionType.neutral);
        this.secondSkill = ("2� skill", ActionType.neutral);
        this.thirdSkill = ("3� skill", ActionType.neutral);
    }


    #region Control de da�o
    public virtual void ReceiveDamage(int damage) // If changed: reflect in Archer and Mage override
    {
        Debug.Log("My health before the attack: " + unit.life);
        int newHealth = unit.life - damage;
        if (newHealth < 1)
        {
            unit.life = 0;
            Die();
        }
        else
        {
            unit.life = newHealth;
        }
        Debug.Log("My health after the attack: " +  unit.life);
    }

    public void ReceiveHealing(int heal)
    {
        Debug.Log("My health before the healing: " +  unit.life);

        int newHealth =  unit.life + heal;
        if (newHealth > MaxHealth)
        {
             unit.life = MaxHealth;
        }
        else
        {
             unit.life= newHealth;
        }
        Debug.Log("My health after the healing: " +  unit.life);
    }

    public void Die()
    {
        Debug.Log("Character died.");
        gm.charactersList.Remove(this);
        gm.auxTransform.Remove(selectedGroundPosition);
        Destroy(selectedGroundPosition.gameObject);
        Destroy(this.gameObject);
    }

    public void GetStunned()
    {
        if (currentStunVFX == null)
        {
            currentStunVFX = Instantiate(stunVFX, transform.position, Quaternion.identity);
        }
        isStunned = true;
    }

    #endregion

    #endregion






}
