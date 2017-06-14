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

            StopCoroutine("update");
            StartCoroutine("update");
        }

        /// <summary>
        /// 속박에서 벗어남, 개인 활동할 수 있게 만들어줌
        /// </summary>
        public void free()
        {
            if (gameObject.activeSelf)
            {
                StopCoroutine("update");
                StartCoroutine("update");
                this.transform.SetParent(GM.MonsterManager.monsterParent);
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
            GameObject obj = null;
            obj = GM.MonsterManager.workingMonster(EMonster.MRECT, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(0, 150);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MRECT, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(150, 0);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MRECT, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(0, -150);
            obj.transform.SetParent(this.transform);
            obj.SendMessage("fetter");
            obj = GM.MonsterManager.workingMonster(EMonster.MRECT, 0);
            obj.transform.localPosition = transform.localPosition + new Vector3(-150, 0);
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