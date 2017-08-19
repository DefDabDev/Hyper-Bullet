using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleVisualizer : MonoBehaviour {

    [SerializeField]
    private AudioSource _audio;

    [SerializeField]
    private float _lerpScale;

    [SerializeField]
    private float _scale;

    [SerializeField]
    private float _maxValue;

    [SerializeField]
    private float _minValue;

    private float[] spectrumData = new float[256];

    private void Update()
    {
        _audio.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        float value = GetAverrage() * _scale;

        if (value < _minValue)
            value = _minValue;

        if (value > _maxValue)
            value = _maxValue;

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(value, value, 1f), _lerpScale);
    }

    private float GetAverrage()
    {
        float sum = 0f;
        for (int i = 0; i < 256; ++i)
        {
            sum += spectrumData[i];
        }
        return (sum / 256f);
    }
}
