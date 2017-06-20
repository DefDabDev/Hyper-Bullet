using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        public static Hero _hero = null;

        //[HideInInspector]
        public uint[] dmg = new uint[5] { 32, 32, 32, 32, 32 };
        

        void Awake()
        {
            _hero = this;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Debug.Log("Game Over");
            }
        }
    }
}