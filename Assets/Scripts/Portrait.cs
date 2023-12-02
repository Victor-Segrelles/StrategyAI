using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    GameMaster gm;
    public Character character;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        gm.focusedCharacter = character;
        gm.FocusCharacter();
    }
}
