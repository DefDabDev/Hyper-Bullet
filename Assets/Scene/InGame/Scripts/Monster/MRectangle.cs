using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MRectangle : CMonster
    {
        void Awake()
        {
            mSpeed = 3f;
            mHP = 100;
            //v_Monster[0].Add(this);
            //Debug.Log(v_Monster.Count);
        }

        void Update()
        {
            moveToTarget();
        }        
    }
}