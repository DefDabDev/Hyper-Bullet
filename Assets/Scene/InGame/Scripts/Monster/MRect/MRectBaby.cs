﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MRectBaby : CMonster
    {
        void Awake()
        {
            GM.MonsterManager.v_Monster[(int)EMonster.MRECTBABY].Add(this);
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
            if (Vector2.Distance(Hero.Hero._hero.transform.position, this.transform.position) > sitterDist)
            {
                moveToTarget();
            }
        }

        IEnumerator spawnMonster()
        {
            while (!GameTime.timeScale.Equals(0))
            {
                yield return new WaitForSeconds(spawnCount);

                GM.MonsterManager.workingMonster(EMonster.MRECT, 0).transform.position = transform.position;
            }
        }
    }
}