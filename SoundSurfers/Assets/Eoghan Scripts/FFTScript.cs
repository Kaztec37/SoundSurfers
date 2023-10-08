using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class FFTScript : MonoBehaviour
{
    Color myColor = new Color(1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {

       //Create a small array of preten RGB pixel values
        int arraySize = 20;
        Complex[] complexArray = new Complex[arraySize];
        Complex[] outputArray = new Complex[arraySize];

        for (int i = 0; i < arraySize; i++)
        {
            complexArray[i] = new Complex(i, i + 1); // Example complex number (real, imaginary)
        }

        for (int i = 0; i < arraySize; i++)
        {
            Debug.Log("InputArray: " + complexArray[i]);
        }

        outputArray = FastFourierTransform.FFT(complexArray, false);

        for (int i = 0; i < arraySize; i++)
        {
            string a = string.Empty;
            a = outputArray[i].ToString();
            string[] b = a.Split(',');
            //int c = int.Parse(b[0]);

            Debug.Log("0: " + b[0] + " 1: " + b[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
