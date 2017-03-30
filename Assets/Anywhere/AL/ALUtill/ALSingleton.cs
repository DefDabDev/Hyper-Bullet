using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AL.ALUtil
{
    public class ALSingleton<T> where T : ALSingleton<T>, new()
    {
        static T _instance = null;
        /// <summary>
        /// Singleton객체를 리턴 함
        /// </summary>
        public static T instance
        {
            get
            {
                return _instance ?? (_instance = new T());
            }
        }
    }
}