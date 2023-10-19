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
        //GameObject XRCamera = GameObject.Find("CSound");
        csound = GetComponent<CsoundUnity>();

        // if (XRCamera == null)
        // {
        //     Debug.Log("Didnt find camera");

        // }
        // else {          

            //csound = XRCamera.GetComponent<CsoundUnity>();
            if (csound == null){
                Debug.Log("Not found csound");
            } else {
                Debug.Log("csound FOUND !!!!");
            }
            
            r = 50;
            g = 0.2f;
            b = 50;
            csound.SetChannel("red", r);
            csound.SetChannel("green", g);
            csound.SetChannel("blue", b);
            //Debug.Log("found cSound script, RGB set");
        //}
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
        // csound.SetChannel("red", r);
        // csound.SetChannel("green", g);
        // csound.SetChannel("blue", b);
        
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
                g = pixelColor.g;
                b = (int)(pixelColor.b * 255);
                Debug.Log("RGB: " + r + ", " + g * 255 + ", " + b);

                //if (r < 5 && g < 0.2f && b < 5){
                if ( g < 0.2f ){ // For performance errors 
                    r = 50;
                    g = 0.2f;
                    b = 50;
                    csound.SetChannel("red", r);
                    csound.SetChannel("green", g);
                    csound.SetChannel("blue", b);
                    //Debug.Log("RGB passed to csound: " + r + ", " + g + ", " + b);

                }else{
                    csound.SetChannel("red", r);
                    csound.SetChannel("green", g);
                    csound.SetChannel("blue", b);
                    //Debug.Log("RGB passed to csound: " + r + ", " + g+ ", " + b);
                }

                // csound.SetChannel("red", r);
                // csound.SetChannel("green", g);
                // csound.SetChannel("blue", b);
            }
            else
            {
                Debug.Log("No Renderer found");
            }
        }
    }
}
