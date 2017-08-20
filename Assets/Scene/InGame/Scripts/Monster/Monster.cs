using AL.ALUtil;
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
        public uint hp { get { return mHP; } }

        public static float mSpeed_Rect = 2;
        public static float mSpeed_Penta = 2;
        public static float mSpeed_Hexa = 2;

        public static float mHp_Rect = 200;
        public static float mHp_Penta = 250;
        public static float mHp_Hexa = 300;

        public bool alone = false;
        public Vector3 moveVector = Vector3.zero;

        public static float spawnCount = 3;
        public static float sitterDist = 8;

        void OnEnable()
        {
            GM.MonsterManager.monsterSetPostion(this.gameObject);
            mSpeed = 1;
            mHP = 1;
            setTarget();
        }

        void OnDisable()
        {
            alone = false;
            transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// 데미지 수령
        /// </summary>
        /// <param name="amount">받은 데미지 양</param>
        public void receiveDMG(uint amount)
        {
            Debug.Log(amount);
            if (mHP <= amount)
            {
                if (!alone)
                {
                    this.transform.SetParent(GM.MonsterManager.monsterParent);
                    alone = true;
                }

                if (this.transform.childCount > 0)
                {
                    for (int i = this.transform.childCount - 1; i >= 0; i--)
                    {
                        this.transform.GetChild(i).SendMessage("free");
                    }
                }
                CameraShaker.instance.Shake();
                this.gameObject.SetActive(false);
            }
            else
            {
                mHP -= amount;
            }
        }

        /// <summary>
        /// 타겟 선정하기
        /// </summary>
        public void setTarget()
        {
            target = Hero.Hero._hero.transform;
        }

        /// <summary>
        /// 타겟을 향해 이동 (타겟 : 사용자)
        /// </summary>
        public void moveToTarget()
        {
            if (!target.Equals(null))
            {
                moveVector = Vector3.MoveTowards(transform.position, target.position, mSpeed * GameTime.deltaTime * moveVariation);
                transform.position = moveVector;

                if (alone)
                {
                    Vector3 difference = target.position - transform.position;
                    difference.Normalize();
                    float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 90);
                    moveVector = transform.up;
                }
            }
            else
                this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 몬스터 속도 변화 (무기 선택시 사용)
        /// </summary>
        //public static void Variation()
        //{
        //    moveVariation = (moveVariation.Equals(1) ? 0.05f : 1);
        //}

        //void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.CompareTag("Bullet"))
        //    {
        //        receiveDMG((uint)Hero.Hero._hero.dmg);
        //        other.gameObject.SetActive(false);
        //    }
        //}
    }
}
