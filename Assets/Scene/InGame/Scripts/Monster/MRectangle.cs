using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MRectangle : Monster
    {
        void Awake()
        {
            mSpeed = 3f;
            mHP = 100;
            v_Monster.Add(this);
            //Debug.Log(v_Monster.Count);
        }

        void Update()
        {
            moveToTarget();
        }        
    }
}