﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GUN_STATE
{
    SLEEP,
    FIRE,
}

public abstract class GunBehaviour : MonoBehaviour
{
    [SerializeField]
    protected GUN_STATE _state;

    [SerializeField]
    protected Transform _shotPosition;

    [SerializeField]
    protected Sprite _playerImage;

    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected float _speed = 700f;

    [SerializeField]
    protected int _onceShootBullet = 1;

    [SerializeField]
    protected float _fireDelay = 0.5f;

    [SerializeField]
    protected float _shootDelay = 0.2f;

    [SerializeField]
    protected float _aceelScale;

    [SerializeField]
    protected float _deaceelScale;

    [SerializeField]
    protected float _aimAccuracy = 10f;

    [SerializeField]
    protected int _magazineSize = 10;
    protected int _realMagazine = 0;

    private Image _image = null;
    public Image image { get { return _image ?? (_image = cg.gameObject.GetComponent<Image>()); } }

    public CartridgeGenerator cg;

    void Awake()
    {
        if (cg == null)
            cg = GetComponent<CartridgeGenerator>();
    }

    public T GetGun<T>()
    {
        return GetComponent<T>();
    }

    public void PullTrriger(float angle)
    {
        if (_state.Equals(GUN_STATE.FIRE))
            return;

        StartCoroutine("Fire", angle);
    }

    protected abstract IEnumerator Fire(float angle);
    public abstract void ChangeGun();
}