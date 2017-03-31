using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IMonster
    {
        /// <summary>
        /// DMG 받기
        /// </summary>
        /// <param name="amount">감소될 양</param>
        void receiveDMG(uint amount);

        /// <summary>
        /// 공격 대상이 있는 방향으로 움직이기
        /// </summary>
        void moveToTarget();
    }
    
    public abstract class Monster : MonoBehaviour, IMonster
    {
        [Header("< 몬스터 시스템 >")]
        [SerializeField]
        private Transform target;       // 공격 대상
        public static float moveVariation = 1;

        [Header("< 고유 스테이터스 >")]
        [SerializeField]
        protected float mSpeed = 0;     // 이동 속도
        [SerializeField]
        protected uint mHP = 0;         // hp

        //public static List<Monster> v_Monster = new List<Monster>();
        //void Awake()
        //{
        //    v_Monster.Add(this);
        //}

        public void receiveDMG(uint amount)
        {
            mHP -= amount;

            if (mHP <= 0)
                Destroy(this.gameObject);
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