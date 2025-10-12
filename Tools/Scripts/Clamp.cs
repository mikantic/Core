using System;
using UnityEngine;
namespace Core.Tools
{
    [Serializable]
    public class Clamp<T> : Observable<T> where T : IComparable<T>
    {
        public T Max { get; protected set; }
        public T Min { get; protected set; }

        public Clamp(T max, T min)
        {
            Max = max;
            Min = min;
        }

        protected override T PreValueChanged(T value)
        {
            if (value.CompareTo(Max) > 0) value = Max;
            if (value.CompareTo(Min) < 0) value = Min;
            return base.PreValueChanged(value);
        }
    }
}
