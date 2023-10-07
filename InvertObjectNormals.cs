using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertObjectNormals : MonoBehaviour
{
    void Awake()
    {
        InvertSphere();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InvertSphere()
    {
        Vector3[] normals = GetComponent<MeshFilter>().mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        GetComponent<MeshFilter>().sharedMesh.normals = normals;

        int[] triangles =GetComponent<MeshFilter>().sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }

        GetComponent<MeshFilter>().sharedMesh.triangles = triangles;
    }
}
