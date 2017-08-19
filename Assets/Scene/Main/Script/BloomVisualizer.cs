using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class BloomVisualizer : MonoBehaviour {

    [SerializeField]
    private AudioSource _audio;

    [SerializeField]
    private BloomOptimized _bloom;

    [SerializeField]
    private float _scale;

    private float[] spectrumData = new float[256];

    private void Update()
    {
        _audio.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        _bloom.intensity = GetAverrage() * _scale;
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
