using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class EditorManager : MonoBehaviour
    {
        void OnGUI()
        {
            int maxH = 100;
            int idx = 0;

            GUI.Box(new Rect(10, 10 + maxH * idx, 100, maxH), "MRect");
            if (GUI.Button(new Rect(15, 30 + maxH * idx, 90, 20), "Create"))
            {
                MonsterManager.workingMonster();
            }

            Monster.CMonster.mSpeed_Rect = GUI.HorizontalSlider(new Rect(15, 50 + maxH * idx, 90, 30), Monster.CMonster.mSpeed_Rect, 0.1F, 5F);
            GUI.Label(new Rect(15, 60 + maxH * idx, 90, 20), "Sp : " + Monster.CMonster.mSpeed_Rect);

            Monster.CMonster.mHp_Rect = GUI.HorizontalSlider(new Rect(15, 80 + maxH * idx, 90, 30), Monster.CMonster.mHp_Rect, 0, 500);
            GUI.Label(new Rect(15, 90 + maxH * idx, 90, 20), "Hp : " + Monster.CMonster.mHp_Rect);

            idx = 1;

        }
    }
}