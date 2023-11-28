using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private PlayerCamera camera;
    public Target activeUnit;
    public Target focusedUnit;
    public Target selectedUnit;

    private void Start()
    {
        camera = FindObjectOfType<PlayerCamera>();
    }

    public void FocusUnit()
    {
        camera.FocusUnit();
    }
}
