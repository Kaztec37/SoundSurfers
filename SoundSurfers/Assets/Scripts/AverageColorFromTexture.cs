using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AverageColorFromTexture : MonoBehaviour
{
    public VideoClip videoToPlay;
    public Light lSource;

    private Color targetColor;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private Renderer rend;
    private Texture tex;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        rend = GetComponent<Renderer>();

        videoPlayer = gameObject.AddComponent<VideoPlayer>();
  
        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;

        videoPlayer.source = VideoSource.VideoClip;
  
        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }
        Debug.Log("Done Preparing Video");

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
        Texture2D convertedTex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        convertedTex2D.ReadPixels(new Rect(0, 0, videoPlayer.texture.width, videoPlayer.texture.height), 0, 0);
        convertedTex2D.Apply();

        targetColor = CalculateAverageColorFromTexture(convertedTex2D);
        //lSource.color = targetColor;

        Debug.Log("AVG RGB: " +targetColor);
    }

    Color32 CalculateAverageColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        float r = 0;
        float g = 0;
        float b = 0;

        for (int i = 0; i < total; i++)
        {
            r += texColors[i].r;
            g += texColors[i].g;
            b += texColors[i].b;
        }
        return new Color32((byte)(r / total), (byte)(g / total), (byte)(b / total), 0);
    }
}
