using UnityEngine;
using System.Collections;
using Csound;
//using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class EyeTrackingController : MonoBehaviour
{
    public string csdFileName = ("C:/Users/David/Documents/Csound Unity/Csound001/Assets/AinesCustom/csound.csd"); // Replace with your Csound instrument filename
    private CsoundUnity csoundSession;
    private Renderer targetRenderer;

    void Start()
    {
        // Initialize the Csound session
        // csoundSession = new CsoundUnity();
        // csoundSession.SetOutputDeviceName("dac");
        // csoundSession.CompileCsd();
        //
        // // Start Csound
        // csoundSession.Start();
        //
        // // Add control channels
        // csoundSession.AddChannel("red");
        // csoundSession.AddChannel("green");
        // csoundSession.AddChannel("blue");
        // csoundSession.AddChannel("brightness");

        // Reference to the texture you want to analyze (assign in the Unity Inspector)
        targetRenderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        // Get eye tracking data and analyze the texture
        // ... (Your eye tracking and texture analysis logic here)

        // Get the color of the pixel the player is looking at
        Texture2D tex = targetRenderer.material.mainTexture as Texture2D;
        Vector2 pixelUV = targetRenderer.material.GetTextureScale("_MainTex") * targetRenderer.material.GetTextureOffset("_MainTex");
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        Color pixelColor = tex.GetPixel(Mathf.FloorToInt(pixelUV.x), Mathf.FloorToInt(pixelUV.y));

        // Update RGB and brightness based on the analyzed pixel
        float red = pixelColor.r;
        float green = pixelColor.g;
        float blue = pixelColor.b;
        float brightness = (red + green + blue) / 3.0f;

        // Set control channel values in Csound
        csoundSession.SetChannel("red", red);
        csoundSession.SetChannel("green", green);
        csoundSession.SetChannel("blue", blue);
        csoundSession.SetChannel("brightness", brightness);
    }

    void OnDestroy()
    {
        // Cleanup Csound
        // csoundSession.Stop();
        // csoundSession.Dispose();
    }
}