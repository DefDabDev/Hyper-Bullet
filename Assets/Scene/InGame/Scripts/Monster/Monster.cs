using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public abstract class CMonster : MonoBehaviour, IMonster
    {
        [Header("< 몬스터 시스템 >")]
        [SerializeField]
        private Transform target;                   // 공격 대상
        public static float moveVariation = 1;      // 이동 속도 변화

        [Header("< 고유 스테이터스 >")]
        [SerializeField]
        protected float mSpeed = 0;     // 이동 속도
        [SerializeField]
        protected uint mHP = 0;         // hp
        
        void OnEnable()
        {
            mSpeed = 1;
            mHP = 1;
            target = Hero.Hero._hero.transform;
        }

        /// <summary>
        /// 데미지 수령
        /// </summary>
        /// <param name="amount">받은 데미지 양</param>
        public void receiveDMG(uint amount)
        {
            mHP -= amount;

            if (mHP <= 0)
                this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 타겟을 향해 이동 (타겟 : 사용자)
        /// </summary>
        public void moveToTarget()
        {
            if (!target.Equals(null))
                transform.position = Vector3.MoveTowards(transform.position, target.position, mSpeed * Time.deltaTime * moveVariation);
        }

        /// <summary>
        /// 몬스터 속도 변화 (무기 선택시 사용)
        /// </summary>
        public static void Variation()
        {
            moveVariation = (moveVariation == 1 ? 0.03f : 1);
        }
    }
}