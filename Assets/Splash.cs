using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Llama a la función EliminarObjeto después de 2 segundos
        Invoke("EliminarObjeto", 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character")) {
            Debug.Log("splash hit");

            //quitar vida etc;
        }
    }

    void EliminarObjeto()
    {
        // Destruye este objeto
        Destroy(gameObject);
    }
}
