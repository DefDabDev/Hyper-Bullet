using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class EditorManager : MonoBehaviour
    {
        //float monsterSpeed = 0;

        void OnGUI()
        {
            GUI.Box(new Rect(10, 10, 100, 100), "MRectangle");
            if (GUI.Button(new Rect(15, 30, 90, 20), "Create"))
            {
                MonsterManager.workingMonster();
            }
            Monster.CMonster.moveVariation = GUI.HorizontalSlider(new Rect(15, 50, 90, 30), Monster.CMonster.moveVariation, 0.1F, 1F);
            GUI.Label(new Rect(15, 60, 90, 20), "Sp : " + Monster.CMonster.moveVariation);

            Monster.CMonster.moveVariation = GUI.HorizontalSlider(new Rect(15, 80, 90, 30), Monster.CMonster.moveVariation, 0.1F, 1F);
            GUI.Label(new Rect(15, 90, 90, 20), "Hp : " + Monster.CMonster.moveVariation);
            
        }
    }
}