using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class EoghanGranularCsound : MonoBehaviour
{

    public CsoundUnity csound;
    public Camera mainCamera;
    public GameObject NasaSatelliteTexture;
    
    int r;
    float g;
    int b;


    public VideoClip videoToPlay;
   // public Light lSource;

    private Color targetColor;
    private VideoPlayer videoPlayer;
   // private VideoSource videoSource;
    private Renderer rend;
    private Texture tex;
  //  private AudioSource audioSource;
  
    private int QuadWidth;
    private int QUadHeight;

    void Start()
    {
        mainCamera = Camera.main;
        csound = GameObject.Find("CSound").GetComponent<CsoundUnity>();

        rend = GetComponent<Renderer>();
        Vector3 QuadSize = rend.bounds.size;
        
        QuadWidth = (int)QuadSize.x;
        QUadHeight = (int)QuadSize.y;

        r = 50;
        g = 0.2f;
        b = 50;

      //  Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;

        videoPlayer.source = VideoSource.VideoClip;

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();
        
        //Wait until video is prepared
        /*while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }
        Debug.Log("Done Preparing Video");*/

        //Assign the Texture from Video to Material texture
        tex = videoPlayer.texture;
        rend.material.mainTexture = tex;

        //Enable new frame Event
        videoPlayer.sendFrameReadyEvents = true;

        //Subscribe to the new frame Event
       videoPlayer.frameReady += OnNewFrame;

        //Play Video
        videoPlayer.Play();

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
    }

    void OnNewFrame(VideoPlayer source, long frameIdx)
    {


        //Texture2D convertedTex2D = new Texture2D(videoPlayer.texture.width, videoPlayer.texture.height, TextureFormat.ARGB32, false);
        //Texture2D convertedTex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        Texture2D convertedTex2D = new Texture2D(QuadWidth, QUadHeight, TextureFormat.ARGB32, false);
        convertedTex2D.ReadPixels(new Rect(0, 0, videoPlayer.texture.width, videoPlayer.texture.height), 0, 0);
        convertedTex2D.Apply();

        //targetColor = CalculateAverageColorFromTexture(convertedTex2D);
        // lSource.color = targetColor;       
    }

    

    void Update()
    {
    // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    // RaycastHit hit;
    /*    r = 50;
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


                int x = (int)(pixelUV.x * tex.width);
                int y = (int)(pixelUV.y * tex.height);
                Color pixelColor = tex.GetPixel(x, y);
                r = (int)(pixelColor.r * 255);
                g = pixelColor.g;
                b = (int)(pixelColor.b * 255);
                Debug.Log("RGB: " + r + ", " + g + ", " + b);
                if (r < 1 && g == 0 && b < 1)
                {
                    r = 2; g = 0.5f; b = 2;
                }
                csound.SetChannel("red", r);
                csound.SetChannel("green", g);
                csound.SetChannel("blue", b);
            }
            else
            {
                Debug.Log("No Renderer found");

            }
        }*/
    }

    /*Color32 CalculateAverageColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        float red = 0;
        float green = 0;
        float blue = 0;

        for (int i = 0; i < total; i++)
        {
            red += texColors[i].r;
            green += texColors[i].g;
            blue += texColors[i].b;
        }
        return new Color32((byte)(red / total), (byte)(green / total), (byte)(blue / total), 0);
    }*/
}


