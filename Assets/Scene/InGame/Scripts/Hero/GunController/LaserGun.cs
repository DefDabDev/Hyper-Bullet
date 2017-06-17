using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : GunBehaviour
{
    protected override IEnumerator Fire(float angle)
    {
        while(true)
        {

            yield return null;
        }
    }
}
