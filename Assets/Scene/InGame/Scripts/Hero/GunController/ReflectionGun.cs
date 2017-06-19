using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionGun : GunBehaviour
{
    private void Awake()
    {
        _realMagazine = _magazineSize;
        //_fireDelay = 0.1f;
        //_speed = 1400f;
    }

    protected override IEnumerator Fire(float angle)
    {
        if (_state.Equals(GUN_STATE.FIRE))
            yield break;


        _state = GUN_STATE.FIRE;

        for (int i = 0; i < _onceShootBullet; ++i)
        {
            float aim = UnityEngine.Random.Range(angle - _aimAccuracy, angle + _aimAccuracy);
            BulletBehaviour temp = BulletPool.instance.GetRelfectionBullet();
            temp.Shoot(_shotPosition, OWNER.PLAYER, BULLET_EFFECT.NORMAL, _speed, _damage);
            temp.SetRotation(aim);
            temp.gameObject.SetActive(true);
            Cartridge c = cg.GetCartridge();
            c.gameObject.SetActive(true);
            c.transform.localPosition = transform.parent.localPosition;
            c.Emission(angle);
            yield return new WaitForSeconds(_shootDelay);
        }
        --_realMagazine;
        if (_realMagazine < 0)
        {
            UIManager.instance.Reload(_fireDelay);
            _realMagazine = _magazineSize;
            yield return new WaitForSeconds(_fireDelay);
        }
        else
            UIManager.instance.DecreaseGauge(_realMagazine, _shootDelay);
        _state = GUN_STATE.SLEEP;
    }

    public override void ChangeGun()
    {
        UIManager.instance.ChangeUI("ReflectionGun", _magazineSize);
        image.sprite = _playerImage;
        _realMagazine = _magazineSize;
        UIManager.instance.Reload(_fireDelay);
    }
}
