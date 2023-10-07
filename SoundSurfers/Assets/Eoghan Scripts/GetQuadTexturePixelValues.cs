using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetQuadTexturePixelValues : MonoBehaviour
{
    public GameObject quad; // Assign your quad in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        // Assuming your camera is at position (0, 0, 0)
        Vector3 cameraPosition = new Vector3(0, 0, 0);

        Ray ray = new Ray(cameraPosition, transform.forward);
        RaycastHit hitInfo;


        try
        {
            if (renderer != null && renderer.material.mainTexture != null)
            {
                /* Texture2D texture = (Texture2D)renderer.material.mainTexture;

                 int x = texture.width / 2;
                 int y = texture.height / 2;

                 Color pixelColor = texture.GetPixel(x, y);

                 Debug.Log("Pixel Color at center: " + pixelColor);*/

                if (Physics.Raycast(ray, out hitInfo))
                {
                    renderer = hitInfo.collider.GetComponent<Renderer>();
                    Vector2 uv = hitInfo.textureCoord;
                    Color pixelColor = ((Texture2D)renderer.material.mainTexture).GetPixelBilinear(uv.x, uv.y);

                    Debug.Log("Pixel Color at ray hit: " + pixelColor);
                }
            }
            else
            {
                Debug.LogError("Renderer component or texture not found.");
            }

        }
        catch (Exception e)
        {
            Debug.Log("Eoghan exception: " +e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
