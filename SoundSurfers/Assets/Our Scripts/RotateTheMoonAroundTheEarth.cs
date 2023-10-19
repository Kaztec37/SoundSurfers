using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTheMoonAroundTheEarth : MonoBehaviour
{
    public Transform earth;
    public float rotationSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(earth.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

