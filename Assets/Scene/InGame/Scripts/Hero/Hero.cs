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

        [SerializeField]
        Animator gameEnd;

        public HeroAttack _attack;

        void Awake()
        {
            _hero = this;
            _attack = GetComponent<HeroAttack>();
        }

        public uint GetDmg()
        {
            return dmg[_attack._currentGun];
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Debug.Log("Game Over");
                gameEnd.SetTrigger("GameEnd");
            }
        }
    }
}