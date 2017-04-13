using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public abstract class Monster : MonoBehaviour, IMonster
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

        public static List<Monster> v_Monster = new List<Monster>();

        void Awake()
        {
        }

        void OnEnable()
        {
            mSpeed = 1;
            mHP = 1;
        }

        public void receiveDMG(uint amount)
        {
            mHP -= amount;

            if (mHP <= 0)
                this.gameObject.SetActive(false);
        }

        public void moveToTarget()
        {
            if (!target.Equals(null))
                transform.position = Vector3.MoveTowards(transform.position, target.position, mSpeed * Time.deltaTime * moveVariation);
        }

        public static void Variation()
        {
            moveVariation = (moveVariation == 1 ? 0.03f : 1);
        }
    }
}