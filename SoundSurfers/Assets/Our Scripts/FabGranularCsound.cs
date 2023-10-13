using UnityEngine;

public class FabGranularCsound : MonoBehaviour
{
    private Camera mainCamera;
    CsoundUnity csound;
    int r;
    float g;
    int b;

    private Texture2D destTexture;

    void Start()
    {
        mainCamera = Camera.main;
        csound = GetComponent<CsoundUnity>();
        r = 50;
        g = 0.2f;
        b = 50;

        destTexture = new Texture2D(3840, 2160, TextureFormat.RGB24, false);
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
        Debug.Log("RGB: " + r + ", " + g + ", " + b);


        if (Physics.Raycast(ray, out hit))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                Vector2 pixelUV = hit.textureCoord;
                var tex = rend.material.mainTexture;
                
                Graphics.CopyTexture(tex, destTexture);
                
                int x = (int)(pixelUV.x * tex.width);
                int y = (int)(pixelUV.y * tex.height);
                Color pixelColor = destTexture.GetPixel(x, y);
                r = (int)(pixelColor.r * 255);
                g = pixelColor.g ;
                b = (int)(pixelColor.b * 255);
                Debug.Log("RGB: " + r + ", " + g + ", " + b);
                if (r < 1 && g == 0 && b < 1){
                    r = 2; g = 0.5f; b = 2  ;
                }
                csound.SetChannel("red", r);
                csound.SetChannel("green", g);
                csound.SetChannel("blue", b);


            } else {
                Debug.Log("No Renderer found");

            }
        }
    }
}

