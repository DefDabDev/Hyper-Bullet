using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class EditorManager : MonoBehaviour
    {
        float monsterSpeed = 0;

        void OnGUI()
        {
            GUI.Box(new Rect(10, 10, 100, 90), "Monster");            
            if (GUI.Button(new Rect(20, 40, 80, 20), "Create"))
            {
                MonsterManager.workingMonster();
            }
            monsterSpeed = GUI.HorizontalSlider(new Rect(20, 65, 80, 30), monsterSpeed, 0.0F, 10.0F);
        }
    }
}