using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Logging : MonoBehaviour
{
    [SerializeField] private Text logText;
    public static Logging instance;
    private void Awake()
    {
        instance = this;
    }

    public void Log(string message)
    {
        if (logText)
            logText.text = message;
        Debug.Log(message);
    }
}
