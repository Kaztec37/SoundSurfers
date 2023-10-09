using UnityEngine;

public class FabCSound : MonoBehaviour
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

                Debug.Log("RGB: " + pixelColor.r + ", " + pixelColor.g + ", " + pixelColor.b);
                csound.SetChannel("red", pixelColor.r);
                csound.SetChannel("green", pixelColor.g);
                csound.SetChannel("blue", pixelColor.b);
            }
        }
    }
}
