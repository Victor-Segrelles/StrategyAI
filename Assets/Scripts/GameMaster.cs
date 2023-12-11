using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    #region Variables
    //C�mara
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

    public List<Character> allies = new List<Character>();
    public List<Character> enemies = new List<Character>();

    public List<Character> charactersList;

    //Raycast y derivados
    public List<Transform> auxTransform;
    public GameObject auxTransformContainer;

    //Control de personajes y turnos
    private int activeCharacterIndex = 0;
    private int generalTurn = 0;

    public LayerMask ground;

    //M�quina de estados
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
            charactersList[i].selectGroundPosition(auxTransform[i]);
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

                    GetCurrentCharacter().selectGroundPosition(auxTransform[activeCharacterIndex]);
                    
                    changeState(state.neutral);
                    GetCurrentCharacter().Move();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                changeState(state.neutral);
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
        GetCurrentCharacter().startTurn();
        camera.FocusCharacter(GetCurrentCharacter());
    }

    //Termina el turno y pasa al siguiente
    public void EndTurn()
    {
        charactersList[activeCharacterIndex].endTurn();
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

    //Esta funci�n devuelve el personaje actual
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
        //ShuffleList(list); TODO descomentar
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

    //Esta funci�n se encarga de poner el texto por pantalla
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

    //Esta funci�n se ocupa de coger todos los nombres de Character, bien su nombre principal o bien sus habilidades
    private string GetCurrentCharacterName()
    {
        return charactersList[activeCharacterIndex].characterName;
    }

    #endregion


    #region Ejecutoras de acciones
    //Estas funciones se "comunican" con el personaje y les da instrucciones de lo que hacer
    public void Move()
    {
        changeState(state.Moving);
        
        GetCurrentCharacter().WarnMove();
        
        

    }

    public void PerformAction1()
    {
        GetCurrentCharacter().PerformAction1();
        changeState(state.Action);
    }

    public void PerformAction2()
    {
        GetCurrentCharacter().PerformAction2();
        changeState(state.Action);

    }

    public void PerformAction3()
    {
        GetCurrentCharacter().PerformAction3();
        changeState(state.Action);
    }

    #endregion

    //Cambio de estado
    public void changeState(state st)
    {
        if(!GetCurrentCharacter().IsMoving() && !GetCurrentCharacter().IsCastingsSkill())
        {
            currentState = st;
        }
    }

}
