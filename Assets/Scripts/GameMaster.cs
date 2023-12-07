using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Character activeCharacter;
    public LayerMask ground;

    private void Update()
    {
        if (!activeCharacter.selectionFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, ground))
                {
                    if (activeCharacter != null)
                    {
                        // Si activeCharacter tiene una propiedad llamada selectedGroundPosition
                        // asigna hit.point a esa propiedad
                        if (activeCharacter.GetType().GetProperty("selectedGroundPosition") != null)
                        {
                            activeCharacter.GetType().GetProperty("selectedGroundPosition").SetValue(activeCharacter, hit.transform, null);
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
    }

    public void StarTurn()
    {
        activeCharacter.StartTurn();
        if (activeCharacter.isPlayerControlled)
        {
            // show interface according to character type: mage | warrior | archer | cleric
        }
    }

    public void Move()
    {
        activeCharacter.Move();
    }

    public void PerformAction1()
    {
        activeCharacter.PerformAction1();
    }

    public void PerformAction2()
    {
        activeCharacter.PerformAction2();

    }

    public void PerformAction3()
    {
        activeCharacter.PerformAction3();
    }

    public void MageTurn() // TODO: DELETE
    {
        Mage mage = FindObjectOfType<Mage>();
        activeCharacter = mage;
    }

    public void WarriorTurn() // TODO: DELETE
    {
        Warrior warrior = FindObjectOfType<Warrior>();
        activeCharacter = warrior;
    }
}
