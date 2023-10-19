using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SoundInteraction : MonoBehaviour
{
    private AudioSource audioSource; 
    
    private void Start(){
        audioSource = GetComponent<AudioSource>(); 
    }

    public void OnHoverEnter(){
        if (audioSource != null){
            audioSource.Play();
        } 
    }

    public void OnHoverExit(){
        if (audioSource != null && audioSource.isPlaying){
            audioSource.Pause(); 
        }
    }
}
