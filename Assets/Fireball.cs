using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float Velocidad = 50.0F;
    //Variables privadas
    private Rigidbody thisRigidbody;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        float step = Velocidad*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            // Realiza acciones relacionadas con la colisión con el enemigo
            // ...

            // Destruimos la bola de fuego cuando colisiona con un enemigo
            Destroy(gameObject);
        }
    }
}
