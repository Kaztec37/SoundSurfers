using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrionFrameGenerator : MonoBehaviour
{
    public Material sphereMat; // Reference to the sphere's material
    public Texture[] frameTextures; // Array of image frames
    public float fPS = 24f; // Frames per second for the video playback

    private int currentFrameIndex = 0;

    private void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        while (true)
        {
            // Check if we have reached the end of the frames
            if (currentFrameIndex >= frameTextures.Length)
            {
                currentFrameIndex = 0; // Loop back to the beginning
            }

            // Update the sphere's material with the current frame texture
            sphereMat.mainTexture = frameTextures[currentFrameIndex];

            // Calculate the delay for the next frame based on the desired frames per second
            float frameDelay = 1f / fPS;

            // Wait for the specified frame delay
            yield return new WaitForSeconds(frameDelay);

            // Move to the next frame
            currentFrameIndex++;
        }
    }
}
