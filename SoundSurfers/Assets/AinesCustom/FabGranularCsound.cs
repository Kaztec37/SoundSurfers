using UnityEngine;

public class FabGranularCsound : MonoBehaviour
{
    private Camera mainCamera;
    CsoundUnity csound;

    void Start()
    {
        mainCamera = Camera.main;
        csound = GetComponent<CsoundUnity>();
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
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
                int r = (int)(pixelColor.r * 255);
                float g = pixelColor.g ;
                int b = (int)(pixelColor.b * 255);
                Debug.Log("RGB: " + r + ", " + g + ", " + b);
                csound.SetChannel("red", r);
                csound.SetChannel("green", g);
                csound.SetChannel("blue", b);
            }
        }
    }
}
