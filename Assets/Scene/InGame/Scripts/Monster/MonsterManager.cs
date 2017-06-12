using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    using Monster;

    public class MonsterManager : MonoBehaviour
    {
        static GameObject monsterPrefab_R;  // M Rect
        static GameObject monsterPrefab_P;  // M Penta
        static GameObject monsterPrefab_H;  // M Hexa
        static GameObject monsterPrefab_RR;  // M RectRect
        static Transform monsterParent;

        public List<CMonster> v_RectMonster = new List<CMonster>();
        public List<CMonster> v_PentaMonster = new List<CMonster>();
        public List<CMonster> v_HexaMonster = new List<CMonster>();
        public List<CMonster> v_RectRectMonster = new List<CMonster>();
        public static List<List<CMonster>> v_Monster = new List<List<CMonster>>();

        void Awake()
        {
            v_Monster.Add(v_RectMonster);
            v_Monster.Add(v_PentaMonster);
            v_Monster.Add(v_HexaMonster);
            v_Monster.Add(v_RectRectMonster);

            monsterPrefab_R = Resources.Load("Monster/MRect") as GameObject;
            monsterPrefab_P = Resources.Load("Monster/MPenta") as GameObject;
            monsterPrefab_H = Resources.Load("Monster/MHexa") as GameObject;
            monsterPrefab_RR = Resources.Load("Monster/MRect_Rect") as GameObject;
            monsterParent = this.transform;
        }

        void Start()
        {
            //!< 초반에 Pool 크기를 5개씩으로 잡고 생성함
            for (int i = 0; i < 5; i++)
                createMonster(EMonster.MRECT).SetActive(false);
            for (int i = 0; i < 5; i++)
                createMonster(EMonster.MPENTA).SetActive(false);
            for (int i = 0; i < 5; i++)
                createMonster(EMonster.MHEXA).SetActive(false);
            for (int i = 0; i < 5; i++)
                createMonster(EMonster.MRECT).SetActive(false);
        }

        /// <summary>
        /// 몬스터 작동시키기 (Pool 이 충분치 않다면 늘려줌)
        /// </summary>
        public static void workingMonster(EMonster em)
        {
            if (checkRestingMonster(em))
                createMonster(em);
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
                case EMonster.MRECT:
                    obj = Instantiate(monsterPrefab_R) as GameObject;
                    break;
                case EMonster.MPENTA:
                    obj = Instantiate(monsterPrefab_P) as GameObject;
                    break;
                case EMonster.MHEXA:
                    obj = Instantiate(monsterPrefab_H) as GameObject;
                    break;
                case EMonster.MRECTRECT:
                    obj = Instantiate(monsterPrefab_RR) as GameObject;
                    break;
                default:
                    break;
            }
            obj.transform.parent = monsterParent;
            obj.transform.localPosition = new Vector3(
                Hero.Hero._hero.transform.position.x + Random.Range(-1280, 1280),
                Hero.Hero._hero.transform.position.y + Random.Range(-720, 720));
            obj.transform.localScale = Vector3.one;

            return obj;
        }
    }
}