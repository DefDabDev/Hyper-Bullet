using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class MonsterManager : MonoBehaviour
    {
        static GameObject monsterPrefab;
        static Transform monsterParent;

        void Awake()
        {
            monsterPrefab = Resources.Load("Monster/MRect") as GameObject;
            monsterParent = this.transform;
        }

        void Start()
        {
            //!< 초반에 Pool 크기를 20개로 잡고 생성함
            for (int i = 0; i < 20; i++)
                createMonster().SetActive(false);
        }

        /// <summary>
        /// 몬스터 작동시키기 (Pool 이 충분치 않다면 늘려줌)
        /// </summary>
        public static void workingMonster()
        {
            if (checkRestingMonster())
                createMonster();
        }

        /// <summary>
        /// 휴식중인, active false 인 Monster 체크하여 재작동 하게 만듦
        /// </summary>
        /// <returns>
        /// 재작동 시킬 Monster가 없다면 true 반환,
        /// 재작동 시킬 Monster가 있었다면 false 반환
        /// </returns>
        public static bool checkRestingMonster()
        {
            int i = 0;
            for (; i < Monster.Monster.v_Monster.Count; i++)
            {
                if (!Monster.Monster.v_Monster[i].gameObject.activeSelf)
                {
                    Monster.Monster.v_Monster[i].gameObject.SetActive(true);
                    break;
                }
            }

            if (i >= Monster.Monster.v_Monster.Count)
                return true;

            return false;
        }

        /// <summary>
        /// 몬스터 생성
        /// </summary>
        public static GameObject createMonster()
        {
            GameObject obj = Instantiate(monsterPrefab) as GameObject;
            obj.transform.parent = monsterParent;
            obj.transform.localPosition = new Vector3(
                Hero.Hero._hero.transform.position.x + Random.Range(-1280, 1280),
                Hero.Hero._hero.transform.position.y + Random.Range(-720, 720));
            obj.transform.localScale = Vector3.one;
            return obj;
        }
    }
}