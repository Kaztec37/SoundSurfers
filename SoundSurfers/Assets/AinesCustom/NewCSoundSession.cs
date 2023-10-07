using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csound;

public class NewCSoundSession : MonoBehaviour
{
    public string csdFilePath;
    
    
    private CsoundUnity cSoundSession;
    private Renderer targetRenderer;
    
    //Save previous frames RBG Values;
    private Vector3 currentRGBValues;
    private Vector3 oldRGBValues;
    
    private void Start()
    {
        //Initialize cSound
        cSoundSession = GetComponent<CsoundUnity>();
        var channelName1 = cSoundSession.GetChannel("red");

        currentRGBValues = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(CastRay(Camera.main.transform.position, Camera.main.transform.forward),
                out RaycastHit hitInfo))
        {
            currentRGBValues = AssignPixelRbgVector3(hitInfo);
            
            if (currentRGBValues != oldRGBValues)
            {
                cSoundSession.SendScoreEvent("i-1 0 -1");
                SetCSoundChannelValues(cSoundSession, currentRGBValues);
                cSoundSession.SendScoreEvent("i1 0 -1");
                currentRGBValues = oldRGBValues;
            }
            else
            {
                currentRGBValues = oldRGBValues;
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
        hitInfo.collider.TryGetComponent<Renderer>(out var currentRenderer);
        return currentRenderer;
    }
    
    private void SetCSoundChannelValues(CsoundUnity csoundUnity, Vector3 pixelValue)
    {
        double brightness = (pixelValue.x + pixelValue.y + pixelValue.z) / 3.0f;
        
        csoundUnity.SetChannel("red", pixelValue.x);
        csoundUnity.SetChannel("blue", pixelValue.y);
        csoundUnity.SetChannel("green", pixelValue.z);
        csoundUnity.SetChannel("brightness", brightness);
    }

    private Vector3 AssignPixelRbgVector3(RaycastHit hitInfo)
    {
        targetRenderer = GetRendererOnThisFrame(hitInfo);
        var tex2D = (Texture2D)targetRenderer.material.mainTexture;
        var pixelValue = tex2D.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
        
        return  new Vector3(pixelValue.r, pixelValue.g, pixelValue.b);
    }
}
