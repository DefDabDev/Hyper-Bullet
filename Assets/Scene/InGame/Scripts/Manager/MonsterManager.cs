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

        public List<CMonster> v_RectMonster = new List<CMonster>();
        public List<CMonster> v_PentaMonster = new List<CMonster>();
        public List<CMonster> v_HexaMonster = new List<CMonster>();
        public List<CMonster> v_RectBabyMonster = new List<CMonster>();
        public List<CMonster> v_PentaBabyMonster = new List<CMonster>();
        public List<CMonster> v_HexaBabyMonster = new List<CMonster>();
        public static List<List<CMonster>> v_Monster = new List<List<CMonster>>();
        #endregion

        public static bool canSpawn = true;

        void Awake()
        {
            GameTime.timeScale = 1;
            v_RectMonster.Clear();
            v_PentaMonster.Clear();
            v_HexaMonster.Clear();
            v_RectBabyMonster.Clear();
            v_PentaBabyMonster.Clear();
            v_HexaBabyMonster.Clear();
            v_Monster.Clear();

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

            monsterPattern();
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

        /// <summary>
        /// 몬스터 시작 포지션 정해주기
        /// </summary>
        /// <param name="obj">몬스터 대상</param>
        public static void monsterSetPostion(GameObject obj)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    obj.transform.localPosition = new Vector3(
                        -1560,
                        +Random.Range(-1000, 1000));
                    break;
                case 1:
                    obj.transform.localPosition = new Vector3(
                        +1560,
                        +Random.Range(-1000, 1000));
                    break;
                case 2:
                    obj.transform.localPosition = new Vector3(
                        +Random.Range(-1560, 1560),
                        -1000);
                    break;
                case 3:
                    obj.transform.localPosition = new Vector3(
                        +Random.Range(-1560, 1560),
                        +1000);
                    break;
            }
        }

        public int monsterCount()
        {
            int count = 0;
            for (int j = 0; j < v_Monster.Count; j++)
                for (int i = 0; i < v_Monster[j].Count; i++)
                    if (v_Monster[j][i].gameObject.activeSelf)
                        count++;
            return count;

        }

        #region _MONSTER_PATTERN_
        void monsterPattern()
        {
            workRamdom(0, 1);
            workRamdom(0, 1);
            //StartCoroutine(rectPattern());
            //StartCoroutine(pentaPattern());
            //StartCoroutine(hexaPattern());
            //StartCoroutine(rectrectPattern());
            //StartCoroutine(turtlePattern());
            //StartCoroutine(rectsitterPattern());
            //StartCoroutine(pentasitterPattern());
            //StartCoroutine(hexasitterPattern());

            StartCoroutine(originPattern());
            StartCoroutine(originoriginPattern());
            StartCoroutine(originsitterPattern());
        }

        #region _PATTERN_0_
        IEnumerator rectPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 5);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(EMonster.MRECT);

                float spawn_1 = Random.Range(0, 5 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(EMonster.MRECT);

                float spawn_2 = Random.Range(0, 5 - spawn_0 - spawn_1);
                yield return new WaitForSeconds(spawn_2);
                workingMonster(EMonster.MRECT);

                yield return new WaitForSeconds(5 - spawn_0 - spawn_1 - spawn_2);
            }
        }
        IEnumerator pentaPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 5);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(EMonster.MPENTA);

                float spawn_1 = Random.Range(0, 5 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(EMonster.MPENTA);

                yield return new WaitForSeconds(5 - spawn_0 - spawn_1);
            }
        }
        IEnumerator hexaPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 5);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(EMonster.MHEXA);

                yield return new WaitForSeconds(5 - spawn_0);
            }
        }
        IEnumerator rectrectPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 10);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(Monster.EMonster.MRECT, 0).SendMessage("copulation");

                float spawn_1 = Random.Range(0, 10 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(Monster.EMonster.MRECT, 0).SendMessage("copulation");

                yield return new WaitForSeconds(10 - spawn_0 - spawn_1);
            }
        }
        IEnumerator turtlePattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 10);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(Monster.EMonster.MPENTA, 0).SendMessage("copulation");

                float spawn_1 = Random.Range(0, 10 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(Monster.EMonster.MPENTA, 0).SendMessage("copulation");

                yield return new WaitForSeconds(10 - spawn_0 - spawn_1);
            }
        }
        IEnumerator rectsitterPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 10);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(Monster.EMonster.MRECTBABY);

                float spawn_1 = Random.Range(0, 10 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(Monster.EMonster.MRECTBABY);

                yield return new WaitForSeconds(10 - spawn_0 - spawn_1);
            }
        }
        IEnumerator pentasitterPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 10);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(Monster.EMonster.MPENTABABY);

                float spawn_1 = Random.Range(0, 10 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(Monster.EMonster.MPENTABABY);

                yield return new WaitForSeconds(10 - spawn_0 - spawn_1);
            }
        }
        IEnumerator hexasitterPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 10);
                yield return new WaitForSeconds(spawn_0);
                workingMonster(Monster.EMonster.MHEXABABY);

                float spawn_1 = Random.Range(0, 10 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workingMonster(Monster.EMonster.MHEXABABY);

                yield return new WaitForSeconds(10 - spawn_0 - spawn_1);
            }
        }
        #endregion

        #region _PATTERN_1
        IEnumerator originPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 10);
                yield return new WaitForSeconds(spawn_0);
                workRamdom(0, 3);

                float spawn_1 = Random.Range(0, 10 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workRamdom(0, 3);

                float spawn_2 = Random.Range(0, 10 - spawn_0 - spawn_1);
                yield return new WaitForSeconds(spawn_2);
                workRamdom(0, 3);

                yield return new WaitForSeconds(10 - spawn_0 - spawn_1 - spawn_2);
            }
        }
        IEnumerator originoriginPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 15);
                yield return new WaitForSeconds(spawn_0);
                workRamdom(3, 5);

                float spawn_1 = Random.Range(0, 15 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workRamdom(3, 5);

                yield return new WaitForSeconds(15 - spawn_0 - spawn_1);
            }
        }
        IEnumerator originsitterPattern()
        {
            while (true)
            {
                float spawn_0 = Random.Range(0, 15);
                yield return new WaitForSeconds(spawn_0);
                workRamdom(5, 8);

                float spawn_1 = Random.Range(0, 15 - spawn_0);
                yield return new WaitForSeconds(spawn_1);
                workRamdom(5, 8);

                yield return new WaitForSeconds(15 - spawn_0 - spawn_1);
            }
        }

        public void workRamdom(int s, int e)
        {
            if (!canSpawn) return;
            if (monsterCount() >= 10) return;

            switch (Random.Range(s, e))
            {
                case 0:
                    workingMonster(EMonster.MRECT);
                    break;
                case 1:
                    workingMonster(EMonster.MPENTA);
                    break;
                case 2:
                    workingMonster(EMonster.MHEXA);
                    break;
                case 3:
                    workingMonster(Monster.EMonster.MRECT, 0).SendMessage("copulation");
                    break;
                case 4:
                    workingMonster(Monster.EMonster.MPENTA, 0).SendMessage("copulation");
                    break;
                case 5:
                    workingMonster(Monster.EMonster.MRECTBABY);
                    break;
                case 6:
                    workingMonster(Monster.EMonster.MPENTABABY);
                    break;
                case 7:
                    workingMonster(Monster.EMonster.MHEXABABY);
                    break;
                default:
                    Debug.LogError("Monster Worm Random Error : s 또는 e 를 확인");
                    break;
            }
        }

        #endregion

        #endregion
    }
}
