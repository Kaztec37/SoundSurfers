using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class RaycastReadPixel : MonoBehaviour
{
    public RenderTexture myRenderTex;
    private Camera cam;
    private Texture2D currentTex;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            ReadTexture(hit);
        }
    }

    void ReadTexture(RaycastHit hit)
    {
        Renderer rend = hit.transform.GetComponent<Renderer>();
        RenderTexture.active = myRenderTex;
        var meshCollider = hit.collider as MeshCollider;
        
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
            meshCollider == null)
        {
            Debug.Log("Could not read Target Texture");
            return;
        }

        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= myRenderTex.width;
        pixelUV.y *= myRenderTex.height;

        currentTex = new Texture2D(1, 1, TextureFormat.RGBA32, false, true);
        
        currentTex.ReadPixels(new Rect(pixelUV.x, pixelUV.y, 1, 1), 0, 0, false);

        if (currentTex == null) return;
        Color color = currentTex.GetPixel(1,1);
        Debug.Log(color);
    }
}
