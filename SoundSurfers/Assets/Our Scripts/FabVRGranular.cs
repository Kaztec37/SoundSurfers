using UnityEngine;
using Unity.XR.Oculus;

public class FabVRGranular : MonoBehaviour
{
    // private Camera mainCamera;
    GameObject XRCamera;
    CsoundUnity csound;
    int r;
    float g;
    int b;

    void Start()
    {
        // mainCamera = Camera.main;
        GameObject XRCamera = GameObject.Find("CSound");

        if (XRCamera == null)
        {
            Debug.Log("Didnt find camera");

        }
        else {          

            csound = XRCamera.GetComponent<CsoundUnity>();
            
            r = 50;
            g = 0.2f;
            b = 50;
            Debug.Log("found cSound script, RGB set");
        }
    }

    void Update()
    {
        // Get the camera's position and forward vector
        //Vector3 cameraPosition = mainCamera.transform.position;
        //Vector3 cameraForward = mainCamera.transform.forward;

        // Vector3 cameraPosition = XRCamera.transform.position;
        // Vector3 cameraForward = XRCamera.transform.forward;

        Vector3 rayOrigin = transform.position;  // Use the position of your GameObject as the origin.
        Vector3 rayDirection = transform.forward;

        //   Debug.Log("CSound gameobject position is: " + cameraPosition);
        //    Debug.Log("CSound gameobject forward is: " + cameraForward);

        r = 50;
           g = 0.2f;
           b = 50;
           csound.SetChannel("red", r);
           csound.SetChannel("green", g);
           csound.SetChannel("blue", b);
           Debug.Log("RGB: " + r + ", " + g + ", " + b);

           // Create a ray from the camera's position and forward vector
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
                   g = pixelColor.g * 255;
                   b = (int)(pixelColor.b * 255);
                   Debug.Log("RGB: " + r + ", " + g + ", " + b);

                   if (r < 1 && g == 0 && b < 1)
                   {
                       r = 2;
                       g = 0.5f;
                       b = 2;
                   }

                   csound.SetChannel("red", r);
                   csound.SetChannel("green", g);
                   csound.SetChannel("blue", b);
               }
               else
               {
                   Debug.Log("No Renderer found");
               }
           }
    }
}
