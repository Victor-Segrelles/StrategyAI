using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{

    public GameObject fireballPrefab;
    public GameObject splashPrefab;
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("throwFireball", 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void throwFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, this.transform.position, Quaternion.identity);
        Fireball fireballscript = fireball.GetComponent<Fireball>();

        if (fireballscript != null) {
            fireballscript.target = enemy;
        } else {
            Debug.LogError("El prefab de la bola de fuego no tiene el script fireball adjunto.");
        }
    }

    void castSplash()
    {
        Vector3 direccionHaciaObjetivo = enemy.transform.position - transform.position;
        Quaternion rotacionHaciaObjetivo = Quaternion.LookRotation(direccionHaciaObjetivo);

        GameObject splash = Instantiate(splashPrefab, this.transform.position, rotacionHaciaObjetivo);
        Splash splashscript = splash.GetComponent<Splash>();
    }

}
