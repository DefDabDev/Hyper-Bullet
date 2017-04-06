using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {

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
        _gun.PullTrriger(value);
    }

    [SerializeField]
    private GunBehaviour _gun;

    Vector2 _rotateVector;
    const float correction = 90f * Mathf.Deg2Rad;
}
