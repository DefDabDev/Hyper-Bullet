using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class EditorManager : MonoBehaviour
    {
        bool onOff = true;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach (GameObject respawn in GameObject.FindGameObjectsWithTag("Monster"))
                {
                    if (respawn.activeSelf)
                    {
                        if (!respawn.transform.parent.CompareTag("MNG"))
                            respawn.SendMessage("free");
                    }
                }
                foreach (GameObject respawn in GameObject.FindGameObjectsWithTag("Monster"))
                {
                    if (respawn.activeSelf)
                    {
                        if (respawn.transform.parent.CompareTag("MNG"))
                            respawn.SendMessage("receiveDMG", (uint)1000);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                onOff = onOff ? false : true;
            }
        }

        void OnGUI()
        {
            if (onOff)
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

                GUI.Box(new Rect(10 + maxH, 10 + maxH * idx, 100, 50), "MRectRect");
                if (GUI.Button(new Rect(15 + maxH, 30 + maxH * idx, 90, 20), "Create"))
                    MonsterManager.workingMonster(Monster.EMonster.MRECT, 0).SendMessage("copulation");

                idx = 1;

                GUI.Box(new Rect(10 + maxH, 10 + 50 * idx, 100, 50), "MTurtle");
                if (GUI.Button(new Rect(15 + maxH, 30 + 50 * idx, 90, 20), "Create"))
                    MonsterManager.workingMonster(Monster.EMonster.MPENTA, 0).SendMessage("copulation");

                idx = 2;

                GUI.Box(new Rect(10 + maxH, 10 + 50 * idx, 100, 50), "MRectBaby");
                if (GUI.Button(new Rect(15 + maxH, 30 + 50 * idx, 90, 20), "Create"))
                    MonsterManager.workingMonster(Monster.EMonster.MRECTBABY);

                idx = 3;

                GUI.Box(new Rect(10 + maxH, 10 + 50 * idx, 100, 50), "MPentaBaby");
                if (GUI.Button(new Rect(15 + maxH, 30 + 50 * idx, 90, 20), "Create"))
                    MonsterManager.workingMonster(Monster.EMonster.MPENTABABY);

                idx = 4;

                GUI.Box(new Rect(10 + maxH, 10 + 50 * idx, 100, 50), "MHexaBaby");
                if (GUI.Button(new Rect(15 + maxH, 30 + 50 * idx, 90, 20), "Create"))
                    MonsterManager.workingMonster(Monster.EMonster.MHEXABABY);

                // HERO
                GUI.Box(new Rect(600, 10, 100, 170), "Hero");
                Hero.Hero._hero.dmg = (uint)Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(605, 30, 90, 30), Hero.Hero._hero.dmg, 0, 100));
                GUI.Label(new Rect(605, 40, 90, 20), "Dmg : " + Hero.Hero._hero.dmg);
                Hero.HeroMover._moveSpeed = GUI.HorizontalSlider(new Rect(605, 60, 90, 30), Hero.HeroMover._moveSpeed, 0.1F, 10F);
                GUI.Label(new Rect(605, 70, 90, 20), "Sp : " + Hero.HeroMover._moveSpeed);
                GunBehaviour._speed = GUI.HorizontalSlider(new Rect(605, 90, 90, 30), GunBehaviour._speed, 100, 500);
                GUI.Label(new Rect(605, 100, 130, 20), "GSp : " + GunBehaviour._speed);
                GunBehaviour._fireDelay = GUI.HorizontalSlider(new Rect(605, 120, 90, 30), GunBehaviour._fireDelay, 0, 0.5F);
                GUI.Label(new Rect(605, 130, 90, 20), "FD : " + GunBehaviour._fireDelay);
                GunBehaviour._shootDelay = GUI.HorizontalSlider(new Rect(605, 150, 90, 30), GunBehaviour._shootDelay, 0, 0.5F);
                GUI.Label(new Rect(605, 160, 90, 20), "SD : " + GunBehaviour._shootDelay);
            }
        }
    }
}