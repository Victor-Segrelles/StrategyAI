using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
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
    Transform auxTransform;

    //Control de personajes y turnos
    private int activeCharacterIndex = 0;
    private int generalTurn = 0;

    public LayerMask ground;

    //Máquina de estados
    public enum state
    {
        neutral,
        Moving,
        Action
    }

    public state currentState = state.neutral;

    void Start()
    {
        charactersList = generateList(allies, enemies);
        UpdateTurnText();
        StartTurn();
        auxTransform = new GameObject("auxTransform").transform;
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
                    auxTransform.position = hit.point;
                    auxTransform.rotation = Quaternion.identity;

                    GetCurrentCharacter().selectGroundPosition(auxTransform);
                    //Destroy(newTransform.gameObject);
                    changeState(state.neutral);
                    GetCurrentCharacter().Move();
                }
            }
        }     
            
            

        

        if (IsTurnComplete())
        {
            EndTurn();
        }
    }


    //Comprueba si ha terminado el turno
    private bool IsTurnComplete()
    {
        return GetCurrentCharacter().IsMovementCompleted();
    }

    //Inicia un nuevo turno
    private void StartTurn()
    {
        // Reiniciar el estado de movimiento solo para el personaje actual
        GetCurrentCharacter().ResetMovementStatus();

        


    }

    //Termina el turno y pasa al siguiente
    private void EndTurn()
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

    //Genera la lista de personajes
    private List<Character> generateList(List<Character> list1, List<Character> list2)
    {
        PopulateList(alliesContainer, allies);
        PopulateList(enemiesContainer, enemies);
        List<Character> list = new List<Character>(list1);
        list.AddRange(list2);
        ShuffleList(list);
        return list;
    }

    private void PopulateList(GameObject container, List<Character> list)
    {
        Character[] characters = container.GetComponentsInChildren<Character>();
        list.AddRange(characters);
    }

    //Esta función se encarga de poner el texto por pantalla
    private void UpdateTurnText()
    {
        characterText.text = $"Turno de: {GetCurrentCharacterName()}";
        turnText.text = "Turno: " + generalTurn.ToString();
        skillsNaming();
    }

    private void skillsNaming()
    {
        firstSkill.GetComponentInChildren<TMP_Text>().text = GetCurrentCharacter().firstSkill;
        secondSkill.GetComponentInChildren<TMP_Text>().text = GetCurrentCharacter().secondSkill;
        thirdSkill.GetComponentInChildren<TMP_Text>().text = GetCurrentCharacter().thirdSkill;
    }

    //Esta función se ocupa de coger todos los nombres de Character, bien su nombre principal o bien sus habilidades
    private string GetCurrentCharacterName()
    {
        return charactersList[activeCharacterIndex].characterName;
    }

    //Esta función devuelve el personaje actual
    public Character GetCurrentCharacter()
    {

        return charactersList[activeCharacterIndex];
    }

    //Estas funciones se "comunican" con el personaje y les da instrucciones de lo que hacer
    public void Move()
    {
        changeState(state.Moving);
        //print("Hola estoy en el move de GameMaster");
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

    //Cambio de estado
    public void changeState(state st)
    {
        if(!GetCurrentCharacter().IsMoving() && !GetCurrentCharacter().IsCastingsSkill())
        {
            currentState = st;
        }
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


}
