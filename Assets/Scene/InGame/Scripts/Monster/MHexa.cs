using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MHexa : CMonster
    {
        void Awake()
        {
            GM.MonsterManager.v_Monster[(int)EMonster.MHEXA].Add(this);
        }

        void OnEnable()
        {
            setTarget();
            mSpeed = mSpeed_Hexa;
            mHP = (uint)mHp_Hexa;
        }

        void Update()
        {
            moveToTarget();
        }
    }
}