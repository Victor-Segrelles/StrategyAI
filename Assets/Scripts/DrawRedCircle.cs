using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRedCircle : MonoBehaviour
{
    public float radius = 1.0f;
    public int segments = 64;
    public float heightOffset = 0.2f; // Adjust the height offset as needed

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        Material material = new Material(Shader.Find("Standard"));
        material.color = Color.red;
        lineRenderer.material = material;
        lineRenderer.widthMultiplier = 0.1f;

        // Set the initial position
        UpdateCirclePosition();
    }

    void Update()
    {
        // Update the position every frame to follow the transform
        UpdateCirclePosition();
    }

    void UpdateCirclePosition()
    {
        float angle = 0f;
        float deltaAngle = 360f / segments;

        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = transform.position.y + heightOffset;
            float z = transform.position.z + Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += deltaAngle;
        }
    }
}
