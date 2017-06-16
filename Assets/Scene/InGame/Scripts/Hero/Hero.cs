using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        public static Hero _hero = null;

        public int dmg = 32;
        

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