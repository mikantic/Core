using System;
using UnityEngine;

namespace Core.Tools
{
    [Serializable]
    public class Property<T> : Hidden<T>
    {
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

        /// <summary>
        /// access to value implicitly
        /// </summary>
        /// <param name="property"></param>
        public static implicit operator T(Property<T> property) => property.Value;

        public override string ToString() => Value.ToString();
    }
}