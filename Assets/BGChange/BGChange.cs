using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AL.ALUtil;

public class BGChange : ALComponentSingleton<BGChange> {

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Image _bg;

    public void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    public void ChangeBG(Color color)
    {
        _bg.color = color;
        StartCoroutine("Change");
    }

    private IEnumerator Change()
    {
        _bg.transform.localScale = Vector3.zero;
        float timer = 0f;
        while(true)
        {
            if (timer >= 1f)
                break;

            timer += Time.deltaTime;
            _bg.transform.localScale = ALLerp.Lerp(_bg.transform.localScale, Vector3.one, timer);
            yield return null;
        }
        _camera.backgroundColor = _bg.color;
        _bg.transform.localScale = Vector3.zero;
    }
}
