using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageProcessing : MonoBehaviour
{
    public GameObject sphereObject; // Reference to the Sphere GameObject
    private Texture2D capturedTexture;
    private Color[] pixelColors;

    private void Start()
    {
        // Initialize the Texture2D and pixelColors array
        capturedTexture = new Texture2D(1, 1); // Initialize with a 1x1 texture (will be resized later)
        pixelColors = new Color[1]; // Initialize with a single pixel (will be resized later)
    }

    private void Update()
    {
        if (sphereObject != null)
        {
            // Access the material from the sphere object
            Renderer renderer = sphereObject.GetComponent<Renderer>();
            Material sphereMaterial = renderer.material;

            // Get the texture from the material
            Texture mainTexture = sphereMaterial.mainTexture;

            if (mainTexture != null)
            {
                // Get the size of the texture
                int textureWidth = mainTexture.width;
                int textureHeight = mainTexture.height;

                // Resize the Texture2D and pixelColors array if the size changes
                if (capturedTexture.width != textureWidth || capturedTexture.height != textureHeight)
                {
                    capturedTexture.Reinitialize(textureWidth, textureHeight);
                    pixelColors = new Color[textureWidth * textureHeight];
                }

                // Render the texture into the Texture2D
                RenderTexture renderTexture = RenderTexture.GetTemporary(textureWidth, textureHeight);
                Graphics.Blit(mainTexture, renderTexture);
                RenderTexture.active = renderTexture;
                capturedTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
                capturedTexture.Apply();
                RenderTexture.active = null;
                RenderTexture.ReleaseTemporary(renderTexture);

                // Get the pixel colors
                pixelColors = capturedTexture.GetPixels();
                Debug.Log(pixelColors);

                
            }
            else
            {
                Debug.LogWarning("No texture found on the material.");
            }
        }
        else
        {
            Debug.LogWarning("Please assign the Sphere GameObject in the inspector.");
        }
    }
}
