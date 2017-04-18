using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        public static Hero _hero = null;

        void Awake()
        {
            _hero = this;
        }
    }
}