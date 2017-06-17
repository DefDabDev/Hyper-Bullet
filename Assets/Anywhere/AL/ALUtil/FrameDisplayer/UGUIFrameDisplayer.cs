using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UGUIFrameDisplayer : MonoBehaviour {

    [SerializeField]
    Text _text;

    [SerializeField]
    Color textColor = Color.white;

    float deltaTime = 0.0f;

    void Update ()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        UpdateText();
    }

    void UpdateText()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        _text.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }
}
