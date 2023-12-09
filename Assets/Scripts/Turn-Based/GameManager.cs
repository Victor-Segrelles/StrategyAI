using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text characterText; 
    public TMP_Text turnText;
    public Button firstSkill;
    public Button secondSkill;
    public Button thirdSkill;

    //Listas de enemigos y aliados
    public GameObject alliesContainer;
    public GameObject enemiesContainer;

    private List<CharacterPlaceHolder> allies = new List<CharacterPlaceHolder>();
    private List<CharacterPlaceHolder> enemies = new List<CharacterPlaceHolder>();

    public List<CharacterPlaceHolder> charactersList;

    //Control de personajes y turnos
    private int activeCharacterIndex = 0;
    private int generalTurn = 0;

    public LayerMask ground;

    void Start()
    {
        charactersList = generateList(allies, enemies);
        UpdateTurnText();
        StartTurn();
    }

    void Update()
    {
        if (!GetCurrentCharacter().selectionFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, ground))
                {
                    if (GetCurrentCharacter() != null)
                    {
                        // Si activeCharacter tiene una propiedad llamada selectedGroundPosition
                        // asigna hit.point a esa propiedad
                        if (GetCurrentCharacter().GetType().GetProperty("selectedGroundPosition") != null)
                        {
                            GetCurrentCharacter().GetType().GetProperty("selectedGroundPosition").SetValue(GetCurrentCharacter(), hit.transform, null);
                        }
                        else
                        {
                            // Si no tiene una propiedad específica, simplemente muestra la posición en el Debug.Log
                            Debug.Log("Punto de impacto en el plano: " + hit.point);
                        }
                    }
                    else
                    {
                        Debug.LogError("activeCharacter es nulo. Asegúrate de asignar un valor a activeCharacter antes de intentar acceder a sus propiedades.");
                    }
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

        GetCurrentCharacter().checkStatus();


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
    private List<CharacterPlaceHolder> generateList(List<CharacterPlaceHolder> list1, List<CharacterPlaceHolder> list2)
    {
        PopulateList(alliesContainer, allies);
        PopulateList(enemiesContainer, enemies);
        List<CharacterPlaceHolder> list = new List<CharacterPlaceHolder>(list1);
        list.AddRange(list2);
        ShuffleList(list);
        return list;
    }
    
    private void PopulateList(GameObject container, List<CharacterPlaceHolder> list)
    {
        CharacterPlaceHolder[] characters = container.GetComponentsInChildren<CharacterPlaceHolder>();
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
    public CharacterPlaceHolder GetCurrentCharacter()
    {
        
        return charactersList[activeCharacterIndex];        
    }

    //Estas funciones se "comunican" con el personaje y les da instrucciones de lo que hacer
    public void Move()
    {
        GetCurrentCharacter().MoveForward();
    }

    public void PerformAction1()
    {
        GetCurrentCharacter().PerformAction1();
    }

    public void PerformAction2()
    {
        GetCurrentCharacter().PerformAction2();

    }

    public void PerformAction3()
    {
        GetCurrentCharacter().PerformAction3();
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