﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBehaviour
{
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

    public override void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect)
    {
        _state = BULLET_STATE.SHOOTING;
        transform.rotation = ownerTransfrom.rotation;
        transform.position = ownerTransfrom.position;
        _bulletEffect = effect;
        _owner = owner;
    }

    public override void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect, float speed, int damage)
    {
        _state = BULLET_STATE.SHOOTING;
        transform.position = ownerTransfrom.position;
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

        if (Hero.Hero._hero.transform.localPosition.x - (Screen.width * 1.9f) > transform.localPosition.x ||
            Hero.Hero._hero.transform.localPosition.x + (Screen.width * 1.9f) < transform.localPosition.x ||
            Hero.Hero._hero.transform.localPosition.y - (Screen.height * 1.9f) > transform.localPosition.y ||
            Hero.Hero._hero.transform.localPosition.y + (Screen.height * 1.9f) < transform.localPosition.y)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            ParticleSystem effect = EffectPool.instance.GetEffect();
            effect.transform.position = transform.position;
            effect.Play();
            collision.SendMessage("receiveDMG", (uint)_damage);
            gameObject.SetActive(false);
        }
    }

    private void Movement()
    {
        rigid2D.velocity = transform.up * _speed * GameTime.deltaTime;
    }
}