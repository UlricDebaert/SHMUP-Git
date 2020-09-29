using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextArea : MonoBehaviour
{
    public string textArea;

    private void OnGUI()
    {
        textArea = GUI.TextArea(new Rect(10, 10, 200, 100), textArea, 200);
    }
}
