using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GetRGB : MonoBehaviour
{
    public static Material textureMaterial;

    Vector2 samplePoint = new Vector2(0.5f, 0.5f); // Example: Sampling from the center

    Texture2D texture = (Texture2D)textureMaterial.mainTexture;

    // Start is called before the first frame update
    void Start()
    {
        
        Color sampledColor = texture.GetPixelBilinear(samplePoint.x, samplePoint.y);

        float red = sampledColor.r;
        float green = sampledColor.g;
        float blue = sampledColor.b;
        float alpha = sampledColor.a;
    }

    // Update is called once per frame
    void Update()
    {
        samplePoint = new Vector2(0.5f, 0.5f); // Update sample point as needed
        Color sampledColor = texture.GetPixelBilinear(samplePoint.x, samplePoint.y);
        // Use sampledColor as needed
    }
}
