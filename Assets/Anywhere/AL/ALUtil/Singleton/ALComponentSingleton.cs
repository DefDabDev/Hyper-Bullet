using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AL.ALUtil
{
    public class ALComponentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance = null;
        /// <summary>
        /// Singleton객체를 리턴 함
        /// </summary>
        public static T instance { get { return _instance ?? GetInstanceObject(); } }

        void Awake()
        {
            _instance = this as T;
        }

        static T GetInstanceObject()
        {
            _instance = GameObject.FindObjectOfType<T>().GetComponent<T>();
            return _instance ?? null;
        }
    }
}