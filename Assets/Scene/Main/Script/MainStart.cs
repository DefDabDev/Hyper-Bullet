using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainStart : MonoBehaviour {

    [SerializeField]
    private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(
        () =>
        {
            Blind.instance.ReplaceScene("InGame");
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
