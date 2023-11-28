using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameMaster gm;
    private Renderer rend;
    public bool selected;
    public Color highlightedColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        if (selected)
        {
            selected = false;
            gm.selectedUnit = null;
            Reset();
        }
        else
        {
            if (gm.selectedUnit != null)
            {
                gm.selectedUnit.Reset();
                gm.selectedUnit.selected = false;
            }
            selected = true;
            gm.selectedUnit = this;
            Highlight();
        }
    }

    public void Highlight()
    {
        rend.material.color = highlightedColor;
    }

    public void Reset()
    {
        rend.material.color = Color.white;
    }
}
