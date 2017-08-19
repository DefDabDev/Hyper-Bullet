using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AL.ALUtil;
using UnityEngine.SceneManagement;

public class Blind : ALComponentSingleton<Blind> {

    [SerializeField]
    private Image _img;

    private bool _isInit = false;

    private readonly Color _visible = new Color(0f, 0f, 0f, 1f);
    private readonly Color _inVisible = new Color(0f, 0f, 0f, 0f);

    private void Awake()
    {
        if (!_isInit)
        {
            DontDestroyOnLoad(gameObject);
            _isInit = true;
        }
    }

    public void ReplaceScene(string name)
    {
        StartCoroutine("SceneFader", name);
    }

    private IEnumerator SceneFader(string name)
    {
        _img.raycastTarget = true;
        _img.color = _inVisible;
        float time = 0f;

        while (true)
        {
            if (time > 1f)
                break;

            time += Time.deltaTime;
            _img.color = ALLerp.Lerp(_img.color, _visible, time);
            yield return null;
        }
        SceneManager.LoadScene(name);
        time = 0f;
        while (true)
        {
            if (time > 1f)
                break;

            time += Time.deltaTime;
            _img.color = ALLerp.Lerp(_img.color, _inVisible, time);
            yield return null;
        }
        _img.color = _inVisible;
        _img.raycastTarget = false;
    }
}
