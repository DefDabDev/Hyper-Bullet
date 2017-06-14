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

        /// <summary>
        /// 합체 : MRectRect 몬스터를 만드는 과정
        /// </summary>
        public void copulation()
        {
            transform.Rotate(new Vector3(0, 0, 180));
            transform.localScale = new Vector3(1.4f, 1.4f);

            GameObject obj = null;
            obj = GM.MonsterManager.workingMonster(EMonster.MPENTA, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(0, 210);
            obj.transform.localScale = new Vector3(1.4f, 1.4f);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MPENTA, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(145, 65);
            obj.transform.localScale = new Vector3(0.9f,0.9f);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MPENTA, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(-145, 65);
            obj.transform.localScale = new Vector3(0.9f, 0.9f);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MPENTA, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(-105, -95);
            obj.transform.localScale = new Vector3(0.9f, 0.9f);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MPENTA, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(105, -95);
            obj.transform.localScale = new Vector3(0.9f, 0.9f);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
        }

        /// <summary>
        /// 행동 불능으로 만들기
        /// </summary>
        public void fetter()
        {
            StopCoroutine("update");
        }
    }
}