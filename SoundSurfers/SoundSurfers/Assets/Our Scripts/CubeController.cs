using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private CsoundUnity csoundUnity;
    private Rigidbody rb;

    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var forward = transform.TransformDirection(Vector3.forward);
        rb.AddForce(forward);
        csoundUnity.SetChannel("speed", rb.velocity.magnitude / 3f);
    }
}
