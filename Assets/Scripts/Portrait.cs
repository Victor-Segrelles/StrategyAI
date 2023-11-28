using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    GameMaster gm;
    public Target unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        gm.focusedUnit = unit;
        gm.FocusUnit();
    }
}
