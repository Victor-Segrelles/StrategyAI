using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    GameMaster gm;
    public Transform target;
    private float speed = 5f;
    private float rotationSpeed = 10f;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        HandleRotationInput();
        HandleMovementInput();
    }

    private void HandleRotationInput()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");

            // Rotate only around Y-axis
            transform.eulerAngles += new Vector3(0, rotationSpeed * mouseX, 0);
        }
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        movement = Quaternion.Euler(0, transform.eulerAngles.y, 0) * movement;

        transform.position += movement * Time.deltaTime * speed;
    }

    public void FocusCharacter()
    {
        if (gm.focusedCharacter != null)
        {
            target = gm.focusedCharacter.transform;
            transform.position = new Vector3(target.position.x, target.position.y + 15, target.position.z - 15);
            transform.LookAt(target);
        }
    }
}