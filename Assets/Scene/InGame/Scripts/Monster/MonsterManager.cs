using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    using Monster;

    public class MonsterManager : MonoBehaviour
    {
        static GameObject monsterPrefab_R;
        static Transform monsterParent;

        public List<CMonster> v_HexaMonster = new List<CMonster>();
        public List<CMonster> v_RectMonster = new List<CMonster>();
        public static List<List<CMonster>> v_Monster = new List<List<CMonster>>();

        void Awake()
        {
            v_Monster.Add(v_RectMonster);
            v_Monster.Add(v_HexaMonster);

            monsterPrefab_R = Resources.Load("Monster/MRect") as GameObject;
            monsterParent = this.transform;
        }

        void Start()
        {
            //!< 초반에 Pool 크기를 20개로 잡고 생성함
            for (int i = 0; i < 20; i++)
                createMonster(EMonster.MREC).SetActive(false);
        }

        /// <summary>
        /// 몬스터 작동시키기 (Pool 이 충분치 않다면 늘려줌)
        /// </summary>
        public static void workingMonster()
        {
            if (checkRestingMonster(EMonster.MREC))
                createMonster(EMonster.MREC);
        }
        
        /// <summary>
        /// 휴식중인, active false 인 Monster 체크하여 재작동 하게 만듦
        /// </summary>
        /// <param name="em">몬스터 타입</param>
        /// <returns>
        /// 재작동 시킬 Monster가 없다면 true 반환,
        /// 재작동 시킬 Monster가 있었다면 false 반환
        /// </returns>
        public static bool checkRestingMonster(EMonster em)
        {
            int i = 0;
            for (; i < v_Monster[(int)em].Count; i++)
            {
                if (!v_Monster[(int)em][i].gameObject.activeSelf)
                {
                    v_Monster[(int)em][i].gameObject.SetActive(true);
                    break;
                }
            }

            if (i >= v_Monster[(int)em].Count)
                return true;

            return false;
        }

        /// <summary>
        /// 몬스터 생성
        /// </summary>
        /// <param name="em">몬스터 타입</param>
        /// <returns>생성된 몬스터</returns>
        public static GameObject createMonster(EMonster em)
        {
            GameObject obj = null;
            switch (em)
            {
                case EMonster.MREC:
                    obj = Instantiate(monsterPrefab_R) as GameObject;
                    break;
                case EMonster.MHEXA:
                    break;
                default:
                    break;
            }
            obj.transform.parent = monsterParent;
            obj.transform.localPosition = new Vector3(
                Hero.Hero._hero.transform.position.x + Random.Range(-1280, 1280),
                Hero.Hero._hero.transform.position.y + Random.Range(-720, 720));
            obj.transform.localScale = Vector3.one;

            v_Monster[(int)em].Add(obj.GetComponent<CMonster>());

            return obj;
        }
    }
}