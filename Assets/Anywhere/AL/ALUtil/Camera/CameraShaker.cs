using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AL.ALUtil
{
    public enum SPACE_TYPE
    {
        WORD,
        LOCAL,
    }

    public enum SHAKE_OPTION
    {
        WITH,
        ALONE,
    }

    public class CameraShaker : ALComponentSingleton<CameraShaker>
    {
        [SerializeField]
        private SPACE_TYPE _spaceType;
        public SPACE_TYPE spaceType { set { _spaceType = value; } get { return _spaceType; } }

        [SerializeField]
        private SHAKE_OPTION _shakeOption;
        public SHAKE_OPTION shakeOption { set { _shakeOption = value; } get { return _shakeOption; } }

        [SerializeField]
        private float _shakeDuration = 1f;
        public float shakeDuration { set { _shakeDuration = value; } get { return _shakeDuration; } }

        [SerializeField]
        private float _shakeAmount = 1f;
        public float shakeAmount { set { _shakeAmount = value; } get { return _shakeAmount; } }

        [SerializeField]
        private float _decreaseFactor = 1f;
        public float decreaseFactor { set { _decreaseFactor = value; } get { return _decreaseFactor; } }

        [SerializeField]
        private Transform[] _targets;
        public Transform[] targets {  get { return _targets; } }

        private Vector3[] _originPosition;
        private float _shakeDurationValue = 0;
        private bool _isShaking = false;
        public bool isShaking {get { return _isShaking; } }

        void Awake()
        {
            _originPosition = new Vector3[targets.Length];
            for (int i = 0; i < targets.Length; ++i)
            {
                _originPosition[i] = targets[i].localPosition;
            }
        }

        public void Shake()
        {
            _shakeDurationValue = _shakeDuration;
            if (_shakeOption.Equals(SHAKE_OPTION.WITH))
                StartCoroutine("ShakingWith");
            else
                StartCoroutine("ShakingAlone");
        }

        IEnumerator ShakingWith()
        {
            if (_isShaking)
                yield break;

            _isShaking = true;

            while (true)
            {
                if (_shakeDurationValue > 0)
                {
                    Vector3 randomValue = Random.insideUnitSphere * shakeAmount;
                    for (int i = 0; i < _targets.Length; ++i)
                    {
                        if (_spaceType.Equals(SPACE_TYPE.LOCAL))
                            _targets[i].localPosition = Vector3.Lerp(_targets[i].localPosition, _originPosition[i] + randomValue, 0.05f);
                        else
                            _targets[i].position = Vector3.Lerp(_targets[i].position, _originPosition[i] + randomValue, 0.05f);
                    }
                    _shakeDurationValue -= Time.deltaTime * _decreaseFactor;
                }
                else
                {
                    _shakeDurationValue = 0f;
                    for (int i = 0; i < _targets.Length; ++i)
                    {
                        if (_spaceType.Equals(SPACE_TYPE.LOCAL))
                            _targets[i].localPosition = _originPosition[i];
                        else
                            _targets[i].position = _originPosition[i];
                    }
                    _isShaking = false;
                    break;
                }
                yield return null;
            }
        }

        IEnumerator ShakingAlone()
        {
            if (_isShaking)
                yield break;

            _isShaking = true;

            while (true)
            {
                if (_shakeDurationValue > 0)
                {
                    for (int i = 0; i < _targets.Length; ++i)
                    {
                        Vector3 randomValue = Random.insideUnitSphere * shakeAmount;
                        if (_spaceType.Equals(SPACE_TYPE.LOCAL))
                            _targets[i].localPosition = Vector3.Lerp(_targets[i].localPosition, _originPosition[i] + randomValue, 0.05f);
                        else
                            _targets[i].position = Vector3.Lerp(_targets[i].position, _originPosition[i] + randomValue, 0.05f);
                    }
                    _shakeDurationValue -= Time.deltaTime * _decreaseFactor;
                }
                else
                {
                    _shakeDurationValue = 0f;
                    for (int i = 0; i < _targets.Length; ++i)
                    {
                        if (_spaceType.Equals(SPACE_TYPE.LOCAL))
                            _targets[i].localPosition = _originPosition[i];
                        else
                            _targets[i].position = _originPosition[i];
                    }
                    _isShaking = false;
                    break;
                }
                yield return null;
            }
        }
    }
}