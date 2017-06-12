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

            // Monster
            GUI.Box(new Rect(10, 10 + maxH * idx, 100, maxH), "MRect");
            if (GUI.Button(new Rect(15, 30 + maxH * idx, 90, 20), "Create"))
                MonsterManager.workingMonster(Monster.EMonster.MRECT);
            Monster.CMonster.mSpeed_Rect = GUI.HorizontalSlider(new Rect(15, 50 + maxH * idx, 90, 30), Monster.CMonster.mSpeed_Rect, 0.1F, 5F);
            GUI.Label(new Rect(15, 60 + maxH * idx, 90, 20), "Sp : " + Monster.CMonster.mSpeed_Rect);
            Monster.CMonster.mHp_Rect = GUI.HorizontalSlider(new Rect(15, 80 + maxH * idx, 90, 30), Monster.CMonster.mHp_Rect, 0, 500);
            GUI.Label(new Rect(15, 90 + maxH * idx, 90, 20), "Hp : " + Monster.CMonster.mHp_Rect);

            idx = 1;
                        
            GUI.Box(new Rect(10, 10 + maxH * idx, 100, maxH), "MHexa");
            if (GUI.Button(new Rect(15, 30 + maxH * idx, 90, 20), "Create"))
                MonsterManager.workingMonster(Monster.EMonster.MHEXA);
            Monster.CMonster.mSpeed_Hexa = GUI.HorizontalSlider(new Rect(15, 50 + maxH * idx, 90, 30), Monster.CMonster.mSpeed_Hexa, 0.1F, 5F);
            GUI.Label(new Rect(15, 60 + maxH * idx, 90, 20), "Sp : " + Monster.CMonster.mSpeed_Hexa);
            Monster.CMonster.mHp_Hexa = GUI.HorizontalSlider(new Rect(15, 80 + maxH * idx, 90, 30), Monster.CMonster.mHp_Hexa, 0, 500);
            GUI.Label(new Rect(15, 90 + maxH * idx, 90, 20), "Hp : " + Monster.CMonster.mHp_Hexa);

            idx = 2;
            
            GUI.Box(new Rect(10, 10 + maxH * idx, 100, maxH), "MPenta");
            if (GUI.Button(new Rect(15, 30 + maxH * idx, 90, 20), "Create"))
                MonsterManager.workingMonster(Monster.EMonster.MPENTA);
            Monster.CMonster.mSpeed_Penta = GUI.HorizontalSlider(new Rect(15, 50 + maxH * idx, 90, 30), Monster.CMonster.mSpeed_Penta, 0.1F, 5F);
            GUI.Label(new Rect(15, 60 + maxH * idx, 90, 20), "Sp : " + Monster.CMonster.mSpeed_Penta);
            Monster.CMonster.mHp_Penta = GUI.HorizontalSlider(new Rect(15, 80 + maxH * idx, 90, 30), Monster.CMonster.mHp_Penta, 0, 500);
            GUI.Label(new Rect(15, 90 + maxH * idx, 90, 20), "Hp : " + Monster.CMonster.mHp_Penta);

            idx = 0;

            GUI.Box(new Rect(10 + maxH, 10 + maxH * idx, 100, maxH), "MRectRect");
            if (GUI.Button(new Rect(15 + maxH, 30 + maxH * idx, 90, 20), "Create"))
                MonsterManager.workingMonster(Monster.EMonster.MRECTRECT);
            Monster.CMonster.mSpeed_Rect = GUI.HorizontalSlider(new Rect(15 + maxH, 50 + maxH * idx, 90, 30), Monster.CMonster.mSpeed_Rect, 0.1F, 5F);
            GUI.Label(new Rect(15, 60 + maxH * idx, 90, 20), "Sp : " + Monster.CMonster.mSpeed_Rect);
            Monster.CMonster.mHp_Rect = GUI.HorizontalSlider(new Rect(15 + maxH, 80 + maxH * idx, 90, 30), Monster.CMonster.mHp_Rect, 0, 500);
            GUI.Label(new Rect(15, 90 + maxH * idx, 90, 20), "Hp : " + Monster.CMonster.mHp_Rect);


            // HERO
            GUI.Box(new Rect(510, 10, 100, 80), "Hero");

            Monster.CMonster.mSpeed_Rect = GUI.HorizontalSlider(new Rect(515, 30, 90, 30), Monster.CMonster.mSpeed_Rect, 0.1F, 5F);
            GUI.Label(new Rect(515, 40, 90, 20), "Sp : " + Monster.CMonster.mSpeed_Rect);

            Monster.CMonster.mHp_Rect = GUI.HorizontalSlider(new Rect(515, 60, 90, 30), Monster.CMonster.mHp_Rect, 0, 500);
            GUI.Label(new Rect(515, 70, 90, 20), "Hp : " + Monster.CMonster.mHp_Rect);
        }
    }
}