using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GetQuadTexturePixelValues : MonoBehaviour
{
    //public GameObject quad; // Assign your quad in the Inspector
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {  
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Cast a Ray every Frame, if it hits a renderer get texture Info + RGB color value and print it to the console
        if (Physics.Raycast(CastRay(mainCam.transform.position, mainCam.transform.forward), out RaycastHit hitInfo))
        {
            if(FindRendererOnThisFrame(hitInfo) != null)
            {
                Vector2 uv = hitInfo.textureCoord;
                Color pixelColor = ((Texture2D)FindRendererOnThisFrame(hitInfo).material.mainTexture).GetPixelBilinear(uv.x, uv.y);

                Debug.Log("Pixel Color at ray hit: " + pixelColor);
            }
        }
        else
        {
            Debug.Log("No Raycastinfo, look at the picture");
        }
    }

    //Cast a Ray from a given source
    private Ray CastRay(Vector3 source, Vector3 direction)
    {
        Ray ray = new Ray(source,direction);
        return ray;
    }

    //if hitInfo valid, see if you can find a Renderer on the RaycastHit
    private Renderer FindRendererOnThisFrame(RaycastHit hitInfo)
    {
        hitInfo.collider.TryGetComponent<Renderer>(out var currentRenderer);

        if (currentRenderer == null) { return null; }

        return currentRenderer;
    }
}
