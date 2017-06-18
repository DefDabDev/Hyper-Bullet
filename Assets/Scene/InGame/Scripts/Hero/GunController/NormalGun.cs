using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : GunBehaviour
{
    private void Awake()
    {
        //_fireDelay = 0.15f;
        //_speed = 800f;
    }

    protected override IEnumerator Fire(float angle)
    {
        if (_state.Equals(GUN_STATE.FIRE))
            yield break;

        _state = GUN_STATE.FIRE;
        for (int i = 0; i < _onceShootBullet; ++i)
        {
            float aim = UnityEngine.Random.Range(angle - _aimAccuracy, angle + _aimAccuracy);
            BulletBehaviour temp = BulletPool.instance.GetBullet();
            temp.Shoot(_shotPosition, OWNER.PLAYER, BULLET_EFFECT.NORMAL, _speed, _damage);
            temp.SetRotation(aim);
            temp.gameObject.SetActive(true);
            Cartridge c = cg.GetCartridge();
            c.gameObject.SetActive(true);
            c.transform.localPosition = transform.parent.localPosition;
            c.Emission(angle);
            yield return new WaitForSeconds(_shootDelay);
        }
        yield return new WaitForSeconds(_fireDelay);
        _state = GUN_STATE.SLEEP;
    }

    public override void ChangeGun()
    {
        UIManager.instance.ChangeUI("NormalGun", 1f);
        image.sprite = _playerImage;
    }
}