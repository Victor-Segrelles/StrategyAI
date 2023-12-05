using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    private PlayerCamera camera;
    public Character character;

    void Start()
    {
        camera = FindObjectOfType<PlayerCamera>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        camera.FocusCharacter(character);
    }
}
