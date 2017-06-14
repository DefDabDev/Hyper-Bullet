using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GUN_STATE
{
    SLEEP,
    FIRE,
}

public abstract class GunBehaviour : MonoBehaviour
{

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

    [SerializeField]
    protected GUN_STATE _state;

    [SerializeField]
    protected Transform _shotPosition;

    [SerializeField]
    protected int _damage;

    //[SerializeField]
    public static float _speed = 100;

    [SerializeField]
    protected int _onceShootBullet = 1;

    //[SerializeField]
    public static float _fireDelay = 0.1f;

    //[SerializeField]
    public static float _shootDelay = 0.1f;

    [SerializeField]
    protected float _aceelScale;

    [SerializeField]
    protected float _deaceelScale;
}