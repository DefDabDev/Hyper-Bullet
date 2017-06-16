using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : GunBehaviour
{

    protected override IEnumerator Fire(float angle)
    {
        if (_state.Equals(GUN_STATE.FIRE))
            yield break;

        _state = GUN_STATE.FIRE;

        for (int i = 0; i < _onceShootBullet; ++i)
        {
            BulletBehaviour temp = BulletPool.instance.GetBullet();
            temp.SetRotation(angle);
            temp.Shoot(_shotPosition, OWNER.PLAYER, BULLET_EFFECT.NORMAL, _speed, _damage);
            temp.gameObject.SetActive(true);
            yield return new WaitForSeconds(_shootDelay);
        }
        yield return new WaitForSeconds(_fireDelay);
        _state = GUN_STATE.SLEEP;
    }
}