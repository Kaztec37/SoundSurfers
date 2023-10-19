using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csound;

public class NewCSoundSession : MonoBehaviour
{
    public string csdFilePath;
    CsoundUnity cSoundSession;
    private Renderer targetRenderer;

    //Save previous frames RBG Values;
    private Vector3 currentRGBValues;
    private Vector3 oldRGBValues;
    public Texture2D tex2D;
    
    private void Start()
    {
        //Initialize cSound
        cSoundSession = GetComponent<CsoundUnity>();
        //var channelName1 = cSoundSession.GetChannel("red");

        currentRGBValues = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (cSoundSession != null) {
            if (Physics.Raycast(CastRay(Camera.main.transform.position, Camera.main.transform.forward),
                    out RaycastHit hitInfo))
            {
                currentRGBValues = AssignPixelRbgVector3(hitInfo);
                
                if (currentRGBValues != oldRGBValues)
                {
                    cSoundSession.SendScoreEvent("i-1 0 -1");
            
                    SetCSoundChannelValues(currentRGBValues);
                    cSoundSession.SendScoreEvent("i1 0 -1");
                    currentRGBValues = oldRGBValues;
                }
                else
                {
                    currentRGBValues = oldRGBValues;
                }
            }
        }
    }


    private static Ray CastRay(Vector3 source, Vector3 direction)
    {
        var ray = new Ray(source, direction);
        return ray;
    }

    // private Renderer GetRendererOnThisFrame(RaycastHit hitInfo)
    // {
    //     if (hitInfo.collider.TryGetComponent<Renderer>() == true)
    //     {
    //         Renderer currentRenderer = hitInfo.collider.GetComponent<Renderer>(out Renderer currentRenderer);// out var ))
    //         return currentRenderer;
    //     }
    //     else{return null;}
        
    // }
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
        
        cSoundSession.SetChannel("red", pixelValue.x);
        cSoundSession.SetChannel("blue", pixelValue.y);
        cSoundSession.SetChannel("green", pixelValue.z);
        cSoundSession.SetChannel("brightness", brightness);
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
            }else
            {
                return Vector3.zero;
            }
        }
        return Vector3.zero;
    }
}
