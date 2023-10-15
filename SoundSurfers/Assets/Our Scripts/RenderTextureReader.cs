using UnityEngine;
using UnityEngine.Video;

public class RenderTextureReader : MonoBehaviour
{
    private Camera mainCamera;
    CsoundUnity csound;
    VideoPlayer videoPlayer;
    int r;
    float g;
    int b;

    //private Texture2D destTexture;
    //Texture2D rTex;
    //RenderTexture vTex;

    void Start()
    {
        mainCamera = Camera.main;
        csound = GetComponent<CsoundUnity>();
        r = 50;
        g = 0.2f;
        b = 50;



    	/*
        Texture2D tex = new Texture2D(3840, 2160, TextureFormat.RGBA32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        */
        // Renderer vRend = GetComponent<Renderer>();
        // RenderTexture vTex = (Texture2D)videoPlayer.texture;
        // vRend.material.mainTexture = vTex;

        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Texture2D convertedTex2D = new Texture2D(videoPlayer.texture.width, videoPlayer.texture.height, TextureFormat.ARGB32, false);
        Texture2D convertedTex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        convertedTex2D.ReadPixels(new Rect(0, 0, videoPlayer.texture.width, videoPlayer.texture.height), 0, 0);
        convertedTex2D.Apply();

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
                Texture2D tex = (Texture2D)rend.material.mainTexture;
                
                //Graphics.CopyTexture(tex, rTex);
                
                int x = (int)(pixelUV.x * tex.width);
                int y = (int)(pixelUV.y * tex.height);

                Color pixelColor = tex.GetPixel(x, y);

                Debug.Log($"Getting Pixel value at {pixelUV}");
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

































    /*




    public RenderTexture renderTexture;
    public Vector2 textureSize = new Vector2(3840, 2160); 

    private Vector3 lastMousePosition;
    private Texture2D readTexture;

    void Start()
    {
        if (renderTexture == null)
        {
            Debug.LogError("RenderTexture not assigned.");
            return;
        }

        readTexture = new Texture2D((int)textureSize.x, (int)textureSize.y);
    }

    void Update()
    {
        // Check if the mouse position has changed
        Vector3 currentMousePosition = Input.mousePosition;
        if (currentMousePosition != lastMousePosition)
        {
            lastMousePosition = currentMousePosition;

            // Calculate the position on the RenderTexture
            Vector2 texCoord = currentMousePosition / new Vector2(Screen.width, Screen.height);
            texCoord.x *= renderTexture.width;
            texCoord.y *= renderTexture.height;

            // Read the RGB color from the RenderTexture at the mouse position
            RenderTexture.active = renderTexture;
            readTexture.ReadPixels(new Rect(texCoord.x, texCoord.y, 1, 1), 0, 0);
            readTexture.Apply();
            RenderTexture.active = null;

            int x = (int)(Input.mousePosition.x * readTexture.width);
            int y = (int)(Input.mousePosition.y * readTexture.height);


            // Get the color at the mouse position
            Color pixelColor = readTexture.GetPixel(x, y);

            // Output the RGB values
            Debug.Log("R: " + pixelColor.r + " G: " + pixelColor.g + " B: " + pixelColor.b);
        }
    }
    */


