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

    public enum EMonster
    {
        MRECT = 0,
        MPENTA,
        MHEXA,
        MRECTBABY,
        MPENTABABY,
        MHEXABABY
        /*MRECTRECT,
        MTURTLE*/
    }
}