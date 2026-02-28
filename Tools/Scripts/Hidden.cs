using System;
using UnityEngine;

namespace Core.Tools
{
    [Serializable]
    public class Hidden<T>
    {
        /// <summary>
        /// backed property
        /// </summary>
        [SerializeField]
        protected T _value;
    }
}