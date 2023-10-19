using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GetRGB : MonoBehaviour
{
    public  Material material;


    // Vector2 samplePoint = new Vector2(0.5f, 0.5f); // Example: Sampling from the center

    int x = 0, y = 0;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if(renderer == null)
        {
            Debug.Log("Didnt get the Sphere's renderer");
        }
        else
        {
            material = renderer.material;
            Texture texture = material.mainTexture;

            Texture2D Bla = (Texture2D)texture;
            Color sampledColor = Bla.GetPixel(x, y);

            float red = sampledColor.r;
            float green = sampledColor.g;
            float blue = sampledColor.b;
            float alpha = sampledColor.a;

            Debug.Log("red: " + red + " green: " + green + " blue: " + blue);
        }
       
       
        //texture = (Texture2D)textureMaterial.mainTexture;

        //Color sampledColor = texture.GetPixelBilinear(samplePoint.x, samplePoint.y);

        

        
    }

    // Update is called once per frame
    void Update()
    {
       // samplePoint = new Vector2(0.5f, 0.5f); // Update sample point as needed
       // Color sampledColor = texture.GetPixelBilinear(samplePoint.x, samplePoint.y);
       // Console.WriteLine(sampledColor);
    }
}
