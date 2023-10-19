using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugDisplay : MonoBehaviour
{
    Dictionary<string, string> debugLogs = new Dictionary<string, string>();

    public TMP_Text display;

    /*public void Start()
    {
        display = new GameObject.GetComponent<TextMeshPro>();
    }*/

    /*public void Update()
    {
        Debug.Log("time:" + Time.time);
        Debug.Log(gameObject.name);
    }*/

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if(type == LogType.Log){
            string[] splitString = logString.Split(char.Parse(":"));
            string debugkey = splitString[0];
            string debugValue = splitString.Length > 1 ? splitString[1] : "";

            if (debugLogs.ContainsKey(debugkey))
            {
                debugLogs[debugkey] = debugValue;
            }
            else
            {
                debugLogs.Add(debugkey, debugValue);
            }
        }

        string displayText = "";
        foreach (KeyValuePair<string, string> log in debugLogs)
        {
            if(log.Value == "")
            {
                displayText = log.Key + "\n";
            }
            else
            {
                displayText += log.Key + ": " + log.Value + "\n";
            }
            display.text = displayText;
        }
    }
}
