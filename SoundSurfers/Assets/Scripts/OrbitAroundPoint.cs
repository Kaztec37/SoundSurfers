using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundPoint : MonoBehaviour
{
    public Transform orbitCenter; // Reference to the center point
    public float orbitSpeed = 1.0f; // Speed of the orbit
    public float rotationSpeed = 0.6f;

    void Update()
    {
        // Calculate the position relative to the center point
        Vector3 relativePos = transform.position - orbitCenter.position;

        // Apply the original relative position
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Rotate around the center point using trigonometry
        transform.RotateAround(orbitCenter.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
