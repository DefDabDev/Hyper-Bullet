using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AL.ALUtil
{
    /// <summary>
    /// Normal Singleton
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class ALSingleton<T> where T : ALSingleton<T>, new()
    {
        static T _instance = null;
        public static T instance
        {
            get
            {
                return _instance ?? (_instance = new T());
            }
        }
    }
}