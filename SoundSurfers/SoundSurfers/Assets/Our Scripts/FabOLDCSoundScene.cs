
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csound;

public class FabOLDSoundSession: MonoBehaviour
{   
    public string csdFilePath;
    CsoundUnity cSoundSession;
    public AudioSource RGBAudio;
    public float pitchMultiplier = 1000f; 
    public float amplitudeMultiplier = 0.1f; 
    private Renderer targetRenderer;

    //Save previous frames RBG Values;
    private Vector3 currentRGBValues = Vector3.zero;
    private Vector3 oldRGBValues;
    public Texture2D tex2D;
    
    private void Start()
    {   
        

        //Initialize cSound
        cSoundSession = GetComponent<CsoundUnity>();
        //System.Environment.SetEnvironmentVariable("Path", Application.streamingAssetsPath);
        

        //var channelName1 = cSoundSession.GetChannel("red");

        //currentRGBValues = Vector3.zero;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (cSoundSession != null) {
            if (Physics.Raycast(CastRay(Camera.main.transform.position, Camera.main.transform.forward), out RaycastHit hitInfo))
            {
                currentRGBValues = AssignPixelRbgVector3(hitInfo);
                // Map RGB values to audio parameters.
                // float frequency = (currentRGBValues.x + currentRGBValues.y + currentRGBValues.y) * pitchMultiplier;
                // float amplitude = (currentRGBValues.x + currentRGBValues.y + currentRGBValues.y) * amplitudeMultiplier;

                // Create a Csound score event with the mapped values.
                //string scoreEvent = $"i1 0 1 {frequency} {amplitude}";// "i-1 0 -1"
                string scoreEvent = $"i1 0 10";

                // Send the score event to Csound.
                cSoundSession.SendScoreEvent(scoreEvent);
                SetCSoundChannelValues(currentRGBValues);

                // Debug.Log("frequency is: " + frequency);
                // Debug.Log("amplitude is: " + amplitude);
                Debug.Log("scoreEvent is: " + scoreEvent);
                

                if (RGBAudio != null)RGBAudio.Play();
                
                
                
                // if (currentRGBValues != oldRGBValues)
                // {
                //     cSoundSession.SendScoreEvent("i-1 0 -1");
            
                //     SetCSoundChannelValues(currentRGBValues);
                //     cSoundSession.SendScoreEvent("i1 0 -1");
                //     currentRGBValues = oldRGBValues;
                // }
                // else
                // {
                //     currentRGBValues = oldRGBValues;
                // }
            }
        }
    }
        

    private static Ray CastRay(Vector3 source, Vector3 direction)
    {
        var ray = new Ray(source, direction);
        return ray;
    }
    private Renderer GetRendererOnThisFrame(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent<Renderer>(out Renderer currentRenderer))
        {
            return currentRenderer;
        }
    return null;
}

    private void SetCSoundChannelValues(Vector3 pixelValue)
    {
        double brightness = (pixelValue.x + pixelValue.y + pixelValue.z) / 3.0f;
        
        cSoundSession.SetChannel("k_red", pixelValue.x);
        cSoundSession.SetChannel("k_blue", pixelValue.y);
        cSoundSession.SetChannel("k_green", pixelValue.z);
        cSoundSession.SetChannel("brightness", brightness);

        // Debug.Log("R value is: " + pixelValue.x);
        // Debug.Log("G value is: " + pixelValue.y);
        // Debug.Log("B value is: " + pixelValue.z);


    }

    private Vector3 AssignPixelRbgVector3(RaycastHit hitInfo){
        if (hitInfo.collider != null) {
            targetRenderer = GetRendererOnThisFrame(hitInfo);
            if (targetRenderer != null){

                tex2D = (Texture2D)targetRenderer.material.mainTexture;

                if(tex2D == null){
                    Debug.Log("Exited AssignPixelRGBVector3 early!");
                    return Vector3.zero;
                }

                var pixelValue = tex2D.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);

                Debug.Log("Texture Coord X: " + hitInfo.textureCoord.x);
                Debug.Log("Texture Coord Y: " + hitInfo.textureCoord.y);
                Debug.Log("Pixel Value: " + pixelValue);
                return new Vector3(pixelValue.r, pixelValue.g, pixelValue.b);

                
            }else{
                return Vector3.zero;
            }
        }
        return Vector3.zero;
    }
}

