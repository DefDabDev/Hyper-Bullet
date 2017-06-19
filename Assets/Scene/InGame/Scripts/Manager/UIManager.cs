using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AL.ALUtil;

public class UIManager : ALComponentSingleton<UIManager> {

    [SerializeField]
    private Image _gunGauge;
    public Image gunGuage { set { _gunGauge = value; } get { return _gunGauge; } }

    [SerializeField]
    private Text _gunName;
    public Text gunName { set { _gunName = value; } get { return _gunName; } }

    private float _maxGauge;
    private bool _isDecreasing = false;
    private IEnumerator _stop = null;

    public void ChangeUI(string gunName, float gauge)
    {
        _maxGauge = gauge;
        _gunName.text = gunName;
    }

    public void DecreaseGauge(int currentValue, float delay)
    {
        _stop = DecreaseAnimation(currentValue, delay);
        StartCoroutine(_stop);
    }

    private IEnumerator DecreaseAnimation(int currentValue, float delay)
    {
        StopCoroutine("ReloadAnimation");
        if (_isDecreasing)
        {
            StopCoroutine("DecreaseAnimation");
        }

        _isDecreasing = true;
        float timer = 0f;
        float targetValue = 0f;
        if (currentValue.Equals(0))
            targetValue = 0f;
        else
            targetValue = currentValue / _maxGauge;

        if (delay <= 0)
        {
            SetScaleX(targetValue);
        }
        else
        {
            while (true)
            {
                if (timer >= 1f)
                    break;

                timer += delay;
                float x = ALLerp.Lerp(_gunGauge.transform.localScale.x, targetValue, timer);
                SetScaleX(x);
                yield return null;
            }
        }
        _isDecreasing = false;
    }

    private void SetScaleX(float x)
    {
        _gunGauge.transform.localScale = new Vector3(x, 1f, 1f);
    }

    public void Reload(float reloadTime)
    {
        if (_stop != null)
            StopCoroutine(_stop); 
        StartCoroutine("ReloadAnimation", reloadTime);
    }

    private IEnumerator ReloadAnimation(float reloadTime)
    {
        float timer = 0f;
        while (true)
        {
            if (timer >= 1f)
                break;

            timer += 0.1f / reloadTime;
            _gunGauge.transform.localScale = ALLerp.Lerp(_gunGauge.transform.localScale, Vector3.one, timer);
            yield return null;
        }
    }
}
