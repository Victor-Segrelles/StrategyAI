using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject alliesContainer;
    public GameObject enemiesContainer;
    public TMP_Text turnText;

    private List<Character> allies = new List<Character>();
    private List<Character> enemies = new List<Character>();
    private int activeCharacterIndex = 0;

    void Start()
    {
        PopulateList(alliesContainer, allies);
        PopulateList(enemiesContainer, enemies);
        UpdateTurnText();
        StartTurn();
    }

    void Update()
    {
        if (IsTurnComplete())
        {
            EndTurn();
        }
    }

    private bool IsTurnComplete()
    {
        return GetCurrentCharacter().IsMovementCompleted();
    }

    private void StartTurn()
    {
        // Reiniciar el estado de movimiento solo para el personaje actual
        GetCurrentCharacter().ResetMovementStatus();
    }

    private void EndTurn()
    {
        // Pasar al siguiente personaje
        activeCharacterIndex++;

        if (activeCharacterIndex >= allies.Count + enemies.Count)
        {
            activeCharacterIndex = 0;
            StartTurn();  // Iniciar un nuevo turno cuando se completa un ciclo de turnos
        }

        UpdateTurnText();
    }

    private void PopulateList(GameObject container, List<Character> list)
    {
        Character[] characters = container.GetComponentsInChildren<Character>();
        list.AddRange(characters);
    }

    private void UpdateTurnText()
    {
        turnText.text = $"Turno de: {GetCurrentCharacterName()}";
    }

    private string GetCurrentCharacterName()
    {
        if (activeCharacterIndex < allies.Count)
        {
            return allies[activeCharacterIndex].characterName;
        }
        else
        {
            return enemies[activeCharacterIndex - allies.Count].characterName;
        }
    }

    private Character GetCurrentCharacter()
    {
        if (activeCharacterIndex < allies.Count)
        {
            return allies[activeCharacterIndex];
        }
        else
        {
            return enemies[activeCharacterIndex - allies.Count];
        }
    }
}


















