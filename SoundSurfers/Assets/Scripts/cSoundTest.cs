using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsoundTest : MonoBehaviour
{
    [Range(0.0f, 200.0f)]public double myCurrentValue = 5.0;

    private CsoundUnity csoundUnity;
    private CharacterController controller;

    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();
    }

    void Update()
    {
        csoundUnity.SetChannel("sound1", myCurrentValue);
    }
}
