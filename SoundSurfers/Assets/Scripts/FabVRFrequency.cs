using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabVRFrequency : MonoBehaviour
{
    CsoundUnity csound;
    int r;
    int g;
    int b;

    void Start()
    {
        
        csound = GetComponent<CsoundUnity>();
    }

    void Update()
    {

        Vector3 rayOrigin = transform.position;  // Use the position of your GameObject as the origin.
        Vector3 rayDirection = transform.forward;
        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                Vector2 pixelUV = hit.textureCoord;
                Texture2D tex = (Texture2D)rend.material.mainTexture;

                int x = (int)(pixelUV.x * tex.width);
                int y = (int)(pixelUV.y * tex.height);
                Color pixelColor = tex.GetPixel(x, y);
                r = (int)(pixelColor.r * 255);
                g = (int)(pixelColor.g * 255);
                b = (int)(pixelColor.b * 255);
                Debug.Log("RGB: " + r + ", " + g + ", " + b);
                csound.SetChannel("red", r);
                csound.SetChannel("green", g);
                csound.SetChannel("blue", b);
            }
        }
    }
}
