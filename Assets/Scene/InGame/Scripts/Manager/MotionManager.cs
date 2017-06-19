using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class MotionManager : MonoBehaviour
    {
        [SerializeField]
        Animator bulletInterface;

        float prevDist = 0;
        float curDist = 0;
        float rateChange = 0;

        [SerializeField]
        GameObject[] selectWeaponObj;

        [SerializeField]
        HeroAttack hAttack;

        // 줌 In/Out 방식
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Z))
        //        bulletInterface.SetBool("OnOff", true);
        //    if (Input.GetKeyDown(KeyCode.X))
        //        bulletInterface.SetBool("OnOff", false);

        //    if (Input.touchCount.Equals(2))
        //    {
        //        if (Input.GetTouch(0).phase.Equals(TouchPhase.Moved) &&
        //            Input.GetTouch(1).phase.Equals(TouchPhase.Moved))
        //        {
        //            curDist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        //            rateChange = prevDist - curDist;
        //            if (rateChange > 150)
        //                bulletInterface.SetBool("OnOff", true);
        //            else if (rateChange < -150)
        //                bulletInterface.SetBool("OnOff", false);
        //            prevDist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        //        }
        //        else
        //        {
        //            curDist = 0;
        //            prevDist = 0;
        //            rateChange = 0;
        //        }
        //    }
        //    else if (Input.touchCount.Equals(1))
        //    {
        //        curDist = 0;
        //        prevDist = 0;
        //        rateChange = 0;
        //        Debug.Log(Input.GetTouch(0).position);
        //    }
        //}

        /// <summary>
        /// 총알 선택 UI 창 On/Off (선택 방식)
        /// </summary>
        public void checkSelectBulletUI()
        {
            bulletInterface.SetBool("OnOff", bulletInterface.GetBool("OnOff") ? false : true);
            for (int i = 0; i < MonsterManager.v_Monster.Count; i++)
            {
                for (int j = 0; j < MonsterManager.v_Monster[i].Count; j++)
                {
                    Monster.CMonster.Variation();
                }
            }
        }

        public void selectWeapon(int idx)
        {
            hAttack._currentGun = idx;
            checkWeapon();
            hAttack.ChangeWeapon(idx);
            checkSelectBulletUI();
        }

        public void checkWeapon()
        {
            for (int i = 0; i < 5; i ++)
            {
                if (hAttack._currentGun.Equals(i))
                    selectWeaponObj[i].SetActive(true);
                else
                    selectWeaponObj[i].SetActive(false);
            }
        }
    }
}