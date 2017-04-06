using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameBehaviour, IBullet {

    public void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect)
    {
        _state = BULLET_STATE.SHOOTING;
        transform.rotation = ownerTransfrom.rotation;
        _bulletEffect = effect;
        _owner = owner;
    }

    public void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect, int speed, int damage)
    {
        _state = BULLET_STATE.SHOOTING;
        transform.rotation = ownerTransfrom.rotation;
        _bulletEffect = effect;
        _owner = owner;
        _speed = speed;
        _damage = damage;
    }

    private void OnDisable()
    {
        _state = BULLET_STATE.SLEEP;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rigid2D.velocity = transform.up * _speed * Time.smoothDeltaTime;
    }

    [SerializeField]
    private BULLET_STATE _state;
    public BULLET_STATE state { get { return _state; } }

    [SerializeField]
    private OWNER _owner;
    public OWNER owner { get { return _owner; } }

    [SerializeField]
    private BULLET_EFFECT _bulletEffect;
    public BULLET_EFFECT bulletEffect { get { return _bulletEffect; } }

    [SerializeField]
    private int _damage;
    public int damage { get { return _damage; } }

    [SerializeField]
    private float _speed;
    public float speed { get { return _speed; } }
}
