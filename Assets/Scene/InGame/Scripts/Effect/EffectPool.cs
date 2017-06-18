using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;

public class EffectPool : ALComponentSingleton<EffectPool> {

    [SerializeField]
    private ParticleSystem _effect;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private int _poolCount;
    private List<ParticleSystem> _pool = new List<ParticleSystem>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _poolCount; ++i)
        {
            ParticleSystem effect = Instantiate(_effect, _parent);
            _pool.Add(effect);
        }
    }

    public ParticleSystem GetEffect()
    {
        for (int i = 0; i < _pool.Count; ++i)
        {
            if (!_pool[i].isPlaying)
                return _pool[i];
        }
        ParticleSystem effect = Instantiate(_effect, _parent);
        effect.gameObject.SetActive(false);
        _pool.Add(effect);
        return effect;
    }
}
