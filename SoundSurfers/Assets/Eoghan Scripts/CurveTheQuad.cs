using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CurveTheQuad : MonoBehaviour
{
    Mesh mesh;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            AddCurvature();
            Debug.Log("More steam");
        }

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float curveAmount = Mathf.Sin(vertices[i].x) * 0.2f; // Adjust curvature using a sine function

            vertices[i].z += curveAmount; // Adjust the Z-coordinate based on curvature
        }

        mesh.vertices = vertices;

    }

    void AddCurvature()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float curveAmount = Mathf.Sin(vertices[i].x) * 0.2f; // Adjust curvature using a sine function

            vertices[i].z += curveAmount; // Adjust the Z-coordinate based on curvature
        }

        mesh.vertices = vertices;

    }
}
