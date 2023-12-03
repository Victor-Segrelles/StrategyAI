using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    private CharacterPlaceHolder character;

    public void SetCharacter(CharacterPlaceHolder newCharacter)
    {
        character = newCharacter;
    }

    void Update()
    {
        //print(character.characterName);       //Debug

        if (character != null && Input.GetKeyDown(KeyCode.Space) && !character.IsMoving())
        {
            StartCoroutine(MoveForward());
        }

        if (character != null && Input.GetKeyDown(KeyCode.D) && !character.IsMoving())
        {
            StartCoroutine(Dash());
        }

        if (character != null && Input.GetKeyDown(KeyCode.R) && !character.IsMoving())
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator MoveForward()
    {
        yield return StartCoroutine(character.MoveForward());
    }

    private IEnumerator Dash()
    {
        yield return StartCoroutine(character.Dash());
    }

    private IEnumerator Rotate()
    {
        yield return StartCoroutine(character.Rotate());
    }
}