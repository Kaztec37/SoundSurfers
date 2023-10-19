using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Profiling;

public class PlayAudioFromArrayofInts : MonoBehaviour
{
    public float[] audioSamples; // Array of audio samples

    // Start is called before the first frame update
    void Start()
    {
        int sampleRate = 44100; // Standard sample rate for audio
        float duration = 1.0f; // Duration of the audio in seconds
        int numSamples = (int)(duration * sampleRate); // Total number of samples

        audioSamples = new float[numSamples]; // Initialize the array

        for (int i = 0; i < numSamples; i++)
        {
            float t = (float)i / sampleRate; // Time in seconds

            // Time-varying frequency and harmonic content
            float frequency = Mathf.Lerp(220.0f, 880.0f, t / duration); // Varying frequency
            int numHarmonics = Mathf.FloorToInt(Mathf.Lerp(1, 10, t / duration)); // Varying number of harmonics

            float sampleValue = 0f;
            for (int harmonic = 1; harmonic <= numHarmonics; harmonic++)
            {
                sampleValue += Mathf.Sin(2.0f * Mathf.PI * frequency * harmonic * t) / harmonic; // Varying harmonics
            }

            // Convert float sample value to int (scaling to 16-bit PCM)
            audioSamples[i] = (int)(sampleValue * 32767.0f);
        }

        // Play the audio
        PlayAudio();
    }

    void PlayAudio()
    {
        AudioClip audioClip = AudioClip.Create("CustomAudioClip", audioSamples.Length, 1, 44100, false);
        audioClip.SetData(audioSamples, 0);

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}









