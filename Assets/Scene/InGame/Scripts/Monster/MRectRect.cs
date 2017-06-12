using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MRectRect : CMonster
    {
        [SerializeField]
        MRect mRect_Center;
        [SerializeField]
        MRect[] mRect = new MRect[4];

        void Awake()
        {
            GM.MonsterManager.v_Monster[(int)EMonster.MRECTRECT].Add(this);
        }

        void OnEnable()
        {
            mRect_Center.detention = false;
            for (int i = 0; i < mRect.Length; i++)
            {
                mRect[i].detention = false;
                mRect[i].gameObject.SetActive(true);
            }

            setTarget();
            mSpeed = mSpeed_Rect;
            mHP = (uint)mHp_Rect;
        }

        void Update()
        {
            transform.Rotate(new Vector3(0, 0, -10 * Time.deltaTime));

            moveToTarget();
            if (mRect_Center.hp <= 0)
            {
                for (int i = 0; i < mRect.Length; i++)
                {
                    mRect[i].free();
                }
            }
        }
    }
}
