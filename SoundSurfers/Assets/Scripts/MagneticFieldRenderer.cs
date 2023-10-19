using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MagneticFieldRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 100;

    [Header("Mathematic Values")]
    [Range(0.0f, 10.0f)] public float angleConst = 2f;
    [Range(0.0f, 20.0f)] public float fieldRadiusX = 5f;
    [Range(0.0f, 20.0f)] public float fieldRadiusY = 5f;
    [Range(0.0f, 360.0f)] public float zOffset = 0.0f;

    private void Start()
    {
        lineRenderer.positionCount = numPoints;
        UpdateMagneticField();
    }

    private void Update()
    {
       UpdateMagneticField();
    }

    private void UpdateMagneticField()
    {
        var xOffset = fieldRadiusX;

        for (int i = 0; i < numPoints; i++)
        {
            float angle = angleConst * Mathf.PI * i / numPoints;
            float x = fieldRadiusX * Mathf.Cos(angle);
            float y = fieldRadiusY * Mathf.Sin(angle);
            Vector3 position = new Vector3(x-xOffset, y, 0f+zOffset);
            lineRenderer.SetPosition(i, position);
        }
    }
}
