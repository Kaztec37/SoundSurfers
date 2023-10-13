using UnityEngine;

public class FabGranularCsound : MonoBehaviour
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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        r = 50;
        g = 0.2f;
        b = 50;
        csound.SetChannel("red", r);
        csound.SetChannel("green", g);
        csound.SetChannel("blue", b);

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
                g = pixelColor.g ;
                b = (int)(pixelColor.b * 255);
                Debug.Log("RGB: " + r + ", " + g + ", " + b);
                if (r == 0 && g == 0 && b == 0){
                    r = 1; g = 0.5f; b = 1;
                }
                csound.SetChannel("red", r);
                csound.SetChannel("green", g);
                csound.SetChannel("blue", b);
            }
        }
    }
}
