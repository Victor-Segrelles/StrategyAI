using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    #region Variables
    //
    public Transform parentCharacter;

    //Cámara
    public PlayerCamera camera;

    //Interfaz
    public TMP_Text characterText;
    public TMP_Text turnText;
    public Button firstSkill;
    public Button secondSkill;
    public Button thirdSkill;

    //Listas de enemigos y aliados
    public GameObject alliesContainer;
    public GameObject enemiesContainer;

    private List<Character> allies = new List<Character>();
    private List<Character> enemies = new List<Character>();

    public List<Character> charactersList;

    //Raycast y derivados
    public List<Transform> auxTransform;
    public GameObject auxTransformContainer;

    //Control de personajes y turnos
    private int activeCharacterIndex = 0;
    private int generalTurn = 0;

    public LayerMask ground;
    private int characterLayer;

    //Máquina de estados
    public enum state
    {
        neutral,
        Moving,
        Action
    }

    public enum ActionType
    {
        neutral,
        oneTarget,
        twoTarget,
        threeTarget,
        groundTarget,
        selfTarget,
        allieTarget
    }

    public state currentState = state.neutral;

    public ActionType currentActionType = ActionType.neutral;

    #endregion

    #region Start y Update
    void Start()
    {
        //parentCharacter = transform.parent;
        characterLayer = LayerMask.NameToLayer("character");
        charactersList = generateList(allies, enemies);
        UpdateTurnText();
        StartTurn();
        for(int i = 0; i<charactersList.Count; i++)
        {
            Transform aux = new GameObject("auxTransform"+i).transform;
            aux.SetParent(auxTransformContainer.transform);
            auxTransform.Add(aux);
        }
        for(int i = 0;i<charactersList.Count; i++)
        {
            charactersList[i].SelectMovementPosition(auxTransform[i]);
        }
    }

    private void Update()
    {
        if(currentState == state.Moving && !GetCurrentCharacter().IsMovementCompleted())
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, ground))
                {
                    auxTransform[activeCharacterIndex].position = hit.point;
                    auxTransform[activeCharacterIndex].rotation = Quaternion.identity;

                    GetCurrentCharacter().SelectMovementPosition(auxTransform[activeCharacterIndex]);
                    
                    changeState(state.neutral);
                    GetCurrentCharacter().Move();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                changeState(state.neutral);
            }
        }

        if (currentState == state.Action && !GetCurrentCharacter().SkillCompleted())
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Lanzar un rayo desde la posición del clic del mouse
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit))
                {
                    if ( hit.collider.gameObject.layer == characterLayer)
                    {
                        GameObject objetoGolpeado = hit.collider.gameObject;

                        Character personaje = objetoGolpeado.GetComponentInParent<Character>();

                        if (GetCurrentCharacter().selectedCharacters.Contains(personaje))
                        {
                            GetCurrentCharacter().selectedCharacters.Remove(personaje);
                        }
                        else
                        {
                            GetCurrentCharacter().selectedCharacters.Add(personaje);
                        }

                        // Handle the click event - return the parent character
                        Debug.Log("Has golpeado a un personaje: " + personaje.name);

                        return;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                changeState(state.neutral);
                changeActionType(ActionType.neutral);
            }

            if(currentActionType == ActionType.oneTarget && GetCurrentCharacter().selectedCharacters.Count==1)
            {

            }
            else if(currentActionType == ActionType.twoTarget)
            {

            }
            else if(currentActionType == ActionType.threeTarget)
            {

            }
        }
    }

    #endregion

    #region Controladores de turno
    //Inicia un nuevo turno
    private void StartTurn()
    {
        // Reiniciar el estado de movimiento solo para el personaje actual
        GetCurrentCharacter().ResetMovementStatus();
        camera.FocusCharacter(GetCurrentCharacter());
        changeActionType(ActionType.neutral);
        changeState(state.neutral) ;
    }

    //Termina el turno y pasa al siguiente
    public void EndTurn()
    {
        // Pasar al siguiente personaje
        activeCharacterIndex++;

        if (activeCharacterIndex >= charactersList.Count)
        {
            activeCharacterIndex = 0;
            generalTurn++;
            StartTurn();  // Iniciar un nuevo turno cuando se completa un ciclo de turnos
        }

        UpdateTurnText();
        StartTurn();
    }

    //Esta función devuelve el personaje actual
    public Character GetCurrentCharacter()
    {
        return charactersList[activeCharacterIndex];
    }

    #endregion

    #region Metodos de listas
    //Genera la lista de personajes
    private List<Character> generateList(List<Character> list1, List<Character> list2)
    {
        PopulateList(alliesContainer, allies);
        PopulateList(enemiesContainer, enemies);
        List<Character> list = new List<Character>(list1);
        list.AddRange(list2);
        ShuffleList(list); //TODO descomentar
        return list;
    }

    private void PopulateList(GameObject container, List<Character> list)
    {
        Character[] characters = container.GetComponentsInChildren<Character>();
        list.AddRange(characters);
    }

    //Algoritmo para ordenar de forma aleatoria una lista

    void ShuffleList<T>(List<T> lista)
    {
        int n = lista.Count;
        System.Random rng = new System.Random();

        // Aplicar el algoritmo de Fisher-Yates para mezclar la lista
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T valor = lista[k];
            lista[k] = lista[n];
            lista[n] = valor;
        }
    }

    #endregion

    #region Controladores de texto

    //Esta función se encarga de poner el texto por pantalla
    private void UpdateTurnText()
    {
        characterText.text = $"Turno de: {GetCurrentCharacterName()}";
        turnText.text = "Turno: " + generalTurn.ToString();
        skillsNaming();
    }

    private void skillsNaming()
    {
        firstSkill.GetComponentInChildren<TMP_Text>().text = GetCurrentCharacter().firstSkill.Item1;
        secondSkill.GetComponentInChildren<TMP_Text>().text = GetCurrentCharacter().secondSkill.Item1;
        thirdSkill.GetComponentInChildren<TMP_Text>().text = GetCurrentCharacter().thirdSkill.Item1;
    }

    //Esta función se ocupa de coger todos los nombres de Character, bien su nombre principal o bien sus habilidades
    private string GetCurrentCharacterName()
    {
        return charactersList[activeCharacterIndex].characterName;
    }

    #endregion


    #region Ejecutoras de acciones
    //Estas funciones se "comunican" con el personaje y les da instrucciones de lo que hacer
    public void Move()
    {
        if (!GetCurrentCharacter().IsMovementCompleted())
        {
            changeState(state.Moving);

            GetCurrentCharacter().WarnMove();
        }
        
        
        

    }

    public void PerformAction1()
    {
        //print(GetCurrentCharacter().firstSkill.Item2);
        //GetCurrentCharacter().PerformAction1();
        if(GetCurrentCharacter().firstSkill.Item2 != ActionType.selfTarget)
        {
            changeState(state.Action);
            changeActionType(GetCurrentCharacter().firstSkill.Item2);
        }
        GetCurrentCharacter().PerformAction1();
    }

    public void PerformAction2()
    {
        //print(GetCurrentCharacter().secondSkill.Item2);
        //GetCurrentCharacter().PerformAction2();
        if (GetCurrentCharacter().secondSkill.Item2 != ActionType.selfTarget)
        {
            changeState(state.Action);
            changeActionType(GetCurrentCharacter().secondSkill.Item2);
        }


    }

    public void PerformAction3()
    {
        //print(GetCurrentCharacter().thirdSkill.Item2);
        //GetCurrentCharacter().PerformAction3();
        if (GetCurrentCharacter().thirdSkill.Item2 != ActionType.selfTarget)
        {
            changeState(state.Action);
            changeActionType(GetCurrentCharacter().thirdSkill.Item2);
        }

    }

    #endregion


    #region Cambio de estado/acciones
    //Cambio de estado/acciones
    public void changeState(state st)
    {
        if(!GetCurrentCharacter().IsMoving() && !GetCurrentCharacter().IsCastingsSkill())
        {
            currentState = st;
        }
    }

    public void changeActionType(ActionType at)
    {
        if (!GetCurrentCharacter().IsMoving() && !GetCurrentCharacter().IsCastingsSkill())
        {
            currentActionType = at;
        }
    }

    #endregion

}
