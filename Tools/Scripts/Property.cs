using System;
using UnityEngine;

namespace Core.Tools
{
    [Serializable]
    public class Property<T>
    {
        /// <summary>
        /// backed property
        /// </summary>
        [SerializeField]
        private T _value;

        /// <summary>
        /// exposed property
        /// </summary>
        public T Value
        {
            get => _value;
            set => TrySetValue(value);
        }

        /// <summary>
        /// local method to change value if different
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool TrySetValue(T value)
        {
            if (Equals(_value, value)) return false;
            _value = value;
            return true;
        }

        /// <summary>
        /// constructors
        /// </summary>
        /// <param name="value"></param>
        public Property(T value)
        {
            _value = value;
        }
    }
}