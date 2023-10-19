using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ConvertTextureToArrayofComplexNumbers : MonoBehaviour
{
    public Texture2D inputTexture;
    public int width;
    public int height;

    public Complex[] complexArray;

    public float[] audioSamples; // Array of audio samples

    // Start is called before the first frame update
    void Start()
    {
        int sampleRate = 44100; // Standard sample rate for audio
        float duration = 1.0f; // Duration of the audio in seconds
        int numSamples = (int)(duration * sampleRate); // Total number of samples

        MakeComplexArray();

    }

    public void MakeComplexArray()
    {
        Renderer renderer = GetComponent<Renderer>();
        Texture2D inputTexture = (Texture2D)renderer.material.mainTexture;

        width = inputTexture.width;
        height = inputTexture.height;

        // Initialize complex array
        complexArray = new Complex[width * height];

        Color[] pixels = inputTexture.GetPixels();
        int index = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = pixels[y * width + x];
                complexArray[index] = new Complex(pixel.r, pixel.g);
                index++;
            }
        }

        Complex[] ourArray =  FastFourierTransform.FFT(complexArray, false);

        
        //remove imaginary part 
        float[] floatArray = new float[complexArray.Length];

        for (int i = 0; i < complexArray.Length; i++)
        {
            string s = complexArray[i].ToString();
            string[] x = s.Split(',');


            string q = x[0].Substring(1);

            // Debug.Log("Real: " + q);

            float a = float.Parse(q);
            floatArray[i] += a;
        }

        AudioClip audioClip = AudioClip.Create("CustomAudioClip", floatArray.Length, 1, 44100, false);
        audioClip.SetData(floatArray, 0);

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    

}
