using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";
    public float secondsToWait = 3.0f;

    private void ToggleDoorOpen()
    {
        var isOpen = animator.GetBool(boolName);
        animator.SetBool(boolName, !isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        ToggleDoorOpen();
    }

    private void OnTriggerExit(Collider other)
    {
        DoorClose();
    }

    private void DoorClose()
    {
        StartCoroutine(DoorCloseCoRoutine());
    }

    private IEnumerator DoorCloseCoRoutine()
    {
        yield return new WaitForSeconds(secondsToWait);
        
        ToggleDoorOpen();
    }
}
