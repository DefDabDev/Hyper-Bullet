using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Object
{
    public class MPenta : CMonster
    {
        protected bool myself = true;
        public bool detention { set { myself = value; } }

        void Awake()
        {
            if (myself)
            {
                GM.MonsterManager.v_Monster[(int)EMonster.MPENTA].Add(this);
            }
        }

        void OnEnable()
        {
            setTarget();
            mSpeed = mSpeed_Hexa;
            mHP = (uint)mHp_Hexa;

            if (myself)
            {
                StopCoroutine("update");
                StartCoroutine("update");
            }
        }

        /// <summary>
        /// 속박에서 벗어남, 개인 활동할 수 있게 만들어줌
        /// </summary>
        public void free()
        {
            if (gameObject.activeSelf)
            {
                myself = true;
                StopCoroutine("update");
                StartCoroutine("update");
            }
        }

        IEnumerator update()
        {
            while (true)
            {
                transform.Rotate(new Vector3(0, 0, -10 * Time.deltaTime));

                moveToTarget();
                yield return null;
            }
        }
    }
}