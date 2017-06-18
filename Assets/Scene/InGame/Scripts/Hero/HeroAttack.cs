using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [SerializeField]
    private int _currentGun = 0;

    [SerializeField]
    private GunBehaviour[] _guns;

    private Vector2 _rotateVector;
    private const float correction = 90f * Mathf.Deg2Rad;

    private void Start()
    {
        _guns[_currentGun].ChangeGun();
    }

    private void Update()
    {
        shoot();
        _rotateVector = new Vector2(CnInputManager.GetAxis("RotateX"), CnInputManager.GetAxis("RotateY"));
    }

    private void shoot()
    {
        if (_rotateVector.Equals(Vector2.zero))
            return;

        float value = (Mathf.Atan2(_rotateVector.y, _rotateVector.x) - correction) * Mathf.Rad2Deg;
        _guns[_currentGun].PullTrriger(value);
    }

    public void ChangeWeapon(int index)
    {
        _currentGun = index;
        _guns[_currentGun].StopAllCoroutines();
        _guns[_currentGun].ChangeGun();
    }
}