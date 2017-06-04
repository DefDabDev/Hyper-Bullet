using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MRect : CMonster
    {
        void Awake()
        {
            GM.MonsterManager.v_Monster[(int)EMonster.MRECT].Add(this);
        }

        void OnEnable()
        {
            setTarget();
            mSpeed = mSpeed_Rect;
            mHP = (uint)mHp_Rect;
        }

        void Update()
        {
            moveToTarget();
        }        
    }
}