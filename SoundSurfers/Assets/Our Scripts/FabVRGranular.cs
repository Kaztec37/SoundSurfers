using UnityEngine;

public class FabVRGranular : MonoBehaviour
{
    private Camera mainCamera;
    CsoundUnity csound;
    int r;
    float g;
    int b;

    void Start()
    {
        mainCamera = Camera.main;
        csound = GetComponent<CsoundUnity>();
        r = 50;
        g = 0.2f;
        b = 50;
    }

    void Update()
    {
        // Get the camera's position and forward vector
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        r = 50;
        g = 0.2f;
        b = 50;
        csound.SetChannel("red", r);
        csound.SetChannel("green", g);
        csound.SetChannel("blue", b);
        Debug.Log("RGB: " + r + ", " + g + ", " + b);

        // Create a ray from the camera's position and forward vector
        Ray ray = new Ray(cameraPosition, cameraForward);
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
