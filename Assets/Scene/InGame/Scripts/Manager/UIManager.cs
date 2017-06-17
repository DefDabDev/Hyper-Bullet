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

    public void ChangeUI(string gunName, float gauge)
    {
        _maxGauge = gauge;
        _gunName.text = gunName;
    }
}
