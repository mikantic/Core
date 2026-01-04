using System;

namespace Core.Tools
{
    public class Maximum<T> : Observable<T> where T : IComparable
    {
        /// <summary>
        /// maximum value in range
        /// </summary>
        public Observable<T> Max { get; private set; }

        /// <summary>
        /// ensures the new value is range
        /// </summary>
        /// <param name="value"></param>
        protected override void InternalValidation(ref T value)
        {
            if (value.GreaterThan(Max)) value = Max;
        }

        /// <summary>
        /// changes the value to be less than or equal to max value
        /// </summary>
        /// <param name="min"></param>
        protected virtual void ClampToMaxValue(T max)
        {
            if (Value.LessThan(max)) Value = Max;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Maximum(T value, T max) : base(value)
        {
            Max = new(max);
            Max.ValueChanged += ClampToMaxValue;
        }
    }
}