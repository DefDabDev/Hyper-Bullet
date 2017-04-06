using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AXIS
{
    HORIZONTAL,
    VERTICAL
}

public enum OWNER
{
    PLAYER,
}

public enum BULLET_EFFECT
{
    NORMAL,
    SLOW,
}

public enum BULLET_STATE
{
    SLEEP,
    SHOOTING,
}

interface IBullet
{
    void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect);
    void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect, int speed, int damage);
}
