using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//== < Monster Desc > =================================================
//
//  - 사각사각 + 육각육각 + 거북이 몬스터는 각각
//    (RRECT)   (HEXA)   (PENTA) 를 재사용합니다.
//    > 사용시엔 코루틴 copulation 을 호출합니다.
//    > 중심부가 파괴되었을 때엔 코루틴 fetter 를 호출합니다.
//
//  - 베이비시터형 몬스터는 재활용 하지 않습니다.
//    > 귀찮아서..
//
//======================================================================

namespace GM
{
    using Monster;

    public class MonsterManager : MonoBehaviour
    {
        #region _MONSTER_POOL_
        static GameObject monsterPrefab_R;  // M Rect
        static GameObject monsterPrefab_P;  // M Penta
        static GameObject monsterPrefab_H;  // M Hexa
        static GameObject monsterPrefab_RB; // M Rect Baby
        static GameObject monsterPrefab_PB; // M Penta Baby
        static GameObject monsterPrefab_HB; // M Hexa Baby
        public static Transform monsterParent;

        public List<CMonster> v_RectMonster     = new List<CMonster>();
        public List<CMonster> v_PentaMonster    = new List<CMonster>();
        public List<CMonster> v_HexaMonster     = new List<CMonster>();
        public List<CMonster> v_RectBabyMonster = new List<CMonster>();
        public List<CMonster> v_PentaBabyMonster = new List<CMonster>();
        public List<CMonster> v_HexaBabyMonster = new List<CMonster>();
        public static List<List<CMonster>> v_Monster = new List<List<CMonster>>();
        #endregion

        void Awake()
        {
            v_Monster.Add(v_RectMonster);
            v_Monster.Add(v_PentaMonster);
            v_Monster.Add(v_HexaMonster);
            v_Monster.Add(v_RectBabyMonster);
            v_Monster.Add(v_PentaBabyMonster);
            v_Monster.Add(v_HexaBabyMonster);

            monsterPrefab_R = Resources.Load("Monster/MRect") as GameObject;
            monsterPrefab_P = Resources.Load("Monster/MPenta") as GameObject;
            monsterPrefab_H = Resources.Load("Monster/MHexa") as GameObject;
            monsterPrefab_RB = Resources.Load("Monster/MRectBaby") as GameObject;
            monsterPrefab_PB = Resources.Load("Monster/MPentaBaby") as GameObject;
            monsterPrefab_PB = Resources.Load("Monster/MHexaBaby") as GameObject;
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
        }

        /// <summary>
        /// 몬스터 작동시키기 (Pool 이 충분치 않다면 늘려줌)
        /// </summary>
        /// <param name="em">몬스터 타입</param>
        public static void workingMonster(EMonster em)
        {
            if (checkRestingMonster(em) < 0)
                createMonster(em);
        }
        /// <summary>
        /// 몬스터 작동시키기 (Pool 이 충분치 않다면 늘려줌)
        /// </summary>
        /// <param name="em">몬스터 타입</param>
        /// <param name="i">기본으로 0 으로 해줄 것</param>
        public static GameObject workingMonster(EMonster em, int i = 0)
        {
            i = checkRestingMonster(em);
            if (i < 0)
                return createMonster(em);
            return v_Monster[(int)em][i].gameObject;
        }

        /// <summary>
        /// 휴식중인, active false 인 Monster 체크하여 재작동 하게 만듦
        /// </summary>
        /// <param name="em">몬스터 타입</param>
        /// <returns>
        /// 재작동 시킬 Monster가 없다면 -1 반환,
        /// 재작동 시킬 Monster가 있었다면 i ( 0 이상 ) 반환
        /// </returns>
        public static int checkRestingMonster(EMonster em)
        {
            int i = 0;
            for (; i < v_Monster[(int)em].Count; i++)
            {
                if (!v_Monster[(int)em][i].gameObject.activeSelf)
                {
                    monsterSetPostion(v_Monster[(int)em][i].gameObject);
                    v_Monster[(int)em][i].gameObject.SetActive(true);
                    break;
                }
            }

            if (i >= v_Monster[(int)em].Count)
                return -1;

            return i;
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
                //case EMonster.MRECTRECT:
                    obj = Instantiate(monsterPrefab_R) as GameObject;
                    break;
                case EMonster.MPENTA:
                //case EMonster.MTURTLE:
                    obj = Instantiate(monsterPrefab_P) as GameObject;
                    break;
                case EMonster.MHEXA:
                    obj = Instantiate(monsterPrefab_H) as GameObject;
                    break;
                case EMonster.MRECTBABY:
                    obj = Instantiate(monsterPrefab_RB) as GameObject;
                    break;
                case EMonster.MPENTABABY:
                    obj = Instantiate(monsterPrefab_PB) as GameObject;
                    break;
                case EMonster.MHEXABABY:
                    obj = Instantiate(monsterPrefab_PB) as GameObject;
                    break;
                default:
                    break;
            }
            obj.transform.SetParent(monsterParent);
            monsterSetPostion(obj);
            obj.transform.localScale = Vector2.one;

            return obj;
        }
        [System.Obsolete("그냥 몬스터 생성만해서 테스트하기 위함, 테스트용이 아니라면 사용하지 마시오.", true)]
        public static GameObject createMonster(EMonster em, Vector2 pos, Vector2 sca)
        {            
            GameObject obj = createMonster(em);
            obj.transform.localPosition = pos;
            obj.transform.localScale = sca;

            return obj;
        }

        static void monsterSetPostion(GameObject obj)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    obj.transform.localPosition = new Vector3(
                        Hero.Hero._hero.transform.position.x + Random.Range(-1400, -1280),
                        Hero.Hero._hero.transform.position.y + Random.Range(720, 900));
                    break;
                case 1:
                    obj.transform.localPosition = new Vector3(
                        Hero.Hero._hero.transform.position.x + Random.Range(-1400, -1280),
                        Hero.Hero._hero.transform.position.y + Random.Range(-900, -720));
                    break;
                case 2:
                    obj.transform.localPosition = new Vector3(
                        Hero.Hero._hero.transform.position.x + Random.Range(1280, 1400),
                        Hero.Hero._hero.transform.position.y + Random.Range(720, 900));
                    break;
                case 3:
                    obj.transform.localPosition = new Vector3(
                        Hero.Hero._hero.transform.position.x + Random.Range(1280, 1400),
                        Hero.Hero._hero.transform.position.y + Random.Range(-900, -720));
                    break;
            }
        }
    }
}