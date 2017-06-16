using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehaviour : GameBehaviour, IBullet
{
    public abstract void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect);
    public abstract void Shoot(Transform ownerTransfrom, OWNER owner, BULLET_EFFECT effect, float speed, int damage);

    public void SetRotation(float z)
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.x, z));
    }
}
