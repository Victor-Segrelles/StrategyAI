using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public string characterName;
    public float advanceDistance = 5f;
    public float rotateAngle = 90f; // Ángulo de giro en grados
    public float dashDistance = 10f;
    public float movementSpeed = 5f;
    private bool isMoving = false;
    private bool movementCompleted = false;

    public IEnumerator MoveForward()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            Vector3 newPosition = transform.position + transform.forward * advanceDistance;
            yield return StartCoroutine(MoveGradually(newPosition));
            isMoving = false;
            movementCompleted = true;
        }
    }

    public IEnumerator Rotate()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, rotateAngle, 0f);
            yield return StartCoroutine(RotateGradually(newRotation));
            isMoving = false;
            movementCompleted = true;
        }
    }

    public IEnumerator Dash()
    {
        if (!isMoving)
        {
            isMoving = true;
            movementCompleted = false;
            Vector3 dashPosition = transform.position + transform.forward * dashDistance;
            yield return StartCoroutine(MoveGradually(dashPosition));
            isMoving = false;
            movementCompleted = true;
        }
    }

    private IEnumerator MoveGradually(Vector3 destination)
    {
        float startTime = Time.time;
        Vector3 initialPosition = transform.position;

        while (Time.time - startTime < advanceDistance / movementSpeed)
        {
            float t = (Time.time - startTime) / (advanceDistance / movementSpeed);
            transform.position = Vector3.Lerp(initialPosition, destination, t);
            yield return null;
        }

        transform.position = destination;
    }

    private IEnumerator RotateGradually(Quaternion targetRotation)
    {
        float startTime = Time.time;
        Quaternion initialRotation = transform.rotation;

        while (Time.time - startTime < 1f)
        {
            float t = (Time.time - startTime);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsMovementCompleted()
    {
        return movementCompleted;
    }

    public void ResetMovementStatus()
    {
        isMoving = false;
        movementCompleted = true;
    }
}
























