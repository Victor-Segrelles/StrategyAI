using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    GameMaster gm;
    public Character character;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        gm.focusedCharacter = character;
        gm.FocusCharacter();
    }
}
