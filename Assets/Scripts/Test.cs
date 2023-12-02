using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        gm.Move();
    }
}
