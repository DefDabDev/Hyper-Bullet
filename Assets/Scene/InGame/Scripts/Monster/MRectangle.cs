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
        }

        void Update()
        {
            moveToTarget();
        }        
    }
}