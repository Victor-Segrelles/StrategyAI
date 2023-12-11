using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Slider slider; 
    [SerializeField] private Camera camera; 
    [SerializeField] private Transform target; 
    [SerializeField] private Vector3 offset;
    public void UpdateBar(float currentValue, float maxHealth)
    {
        if (maxHealth <= 0)
        {
            Debug.LogWarning("MaxHealth debe ser mayor que cero.");
            return;
        }
        
        slider.value=currentValue/ maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position=target.position + offset;
    }
}
