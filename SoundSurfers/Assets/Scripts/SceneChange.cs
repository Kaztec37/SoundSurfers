using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   public int mySceneID;
   public FadeScreen fadeScreen;
   
   public void MoveToScene(int sceneID)
   {
      StartCoroutine(MoveToSceneRoutine(sceneID));
   }

   private IEnumerator MoveToSceneRoutine(int sceneID)
   {
      Debug.Log($"Coroutine MoveToScene {sceneID} has been started");
      fadeScreen.FadeOut();
      yield return new WaitForSeconds(fadeScreen.fadeDuration);
      SceneManager.LoadScene(sceneID);
   }
   
   private void OnTriggerEnter(Collider other)
   {
      MoveToScene(mySceneID);
   }
}
