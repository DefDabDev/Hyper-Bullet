using Monster;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionBullet : BulletBehaviour
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

    private int _reflectionCount = 0;
    private const float correction = 90f * Mathf.Deg2Rad;
    private Vector3 _firePoint;
    private Vector3 _moveVector;
    bool _vecotrMove = false;

    public override void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect)
    {
        _state = BULLET_STATE.SHOOTING;
        transform.rotation = ownerTransfrom.rotation;
        transform.position = ownerTransfrom.position;
        _bulletEffect = effect;
        _owner = owner;
        _firePoint = ownerTransfrom.position;
        _vecotrMove = false;
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
        _firePoint = ownerTransfrom.position;
        _vecotrMove = false;
    }

    private void OnDisable()
    {
        _state = BULLET_STATE.SLEEP;
        _reflectionCount = 3;
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
            float temp = Vector3.Angle(transform.up, collision.GetComponent<CMonster>().moveVector);
            temp = Mathf.Rad2Deg * temp;
            SetRotation(temp);
            //collision.SendMessage("receiveDMG", (uint)_damage);
            collision.SendMessage("receiveDMG", Hero.Hero._hero.dmg);
            --_reflectionCount;
            if (_reflectionCount <= 0)
                gameObject.SetActive(false);
        }

        //if (collision.CompareTag("Edge"))
        //{
        //    RaycastHit2D hit;
        //    if (_vecotrMove)
        //        hit = Physics2D.Raycast(transform.position, _moveVector);
        //    else
        //        hit = Physics2D.Raycast(transform.position, transform.up);
        //    Vector3 inVector = (Vector3)hit.point - _firePoint;
        //    Vector3 outVecor = _firePoint - (Vector3)hit.point;
        //    Vector3 normalVector = hit.normal;
        //    //float temp = Mathf.Atan2(outVecor.y, outVecor.x) * Mathf.Rad2Deg;
        //    _moveVector = Vector3.Reflect(inVector, normalVector);
        //    _moveVector = _moveVector.normalized;
        //    _vecotrMove = true;
        //    //float temp = Vector3.Angle(transform.up, collision.transform.up);
        //    //temp = Mathf.Rad2Deg * temp;
        //    //SetRotation(temp);
        //    --_reflectionCount;
        //    if (_reflectionCount <= 0)
        //        gameObject.SetActive(false);
        //}
    }

    private void Movement()
    {
        if (_vecotrMove)
            rigid2D.velocity = _moveVector * _speed * Time.smoothDeltaTime;
        else
            rigid2D.velocity = transform.up * _speed * Time.smoothDeltaTime;
    }
}
