﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MPentaBaby : CMonster
    {
        void Awake()
        {
            GM.MonsterManager.v_Monster[(int)EMonster.MPENTABABY].Add(this);
        }

        void OnEnable()
        {
            setTarget();
            mSpeed = mSpeed_Rect;
            mHP = (uint)mHp_Rect;

            StopCoroutine("spawnMonster");
            StartCoroutine("spawnMonster");
        }

        void Update()
        {
            if (Vector2.Distance(Hero.Hero._hero.transform.position, this.transform.position) > 6)
            {
                moveToTarget();
            }
        }

        IEnumerator spawnMonster()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);

                GM.MonsterManager.workingMonster(EMonster.MPENTA, 0).transform.position = transform.position;
            }
        }
    }
}