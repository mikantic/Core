using System;

namespace Core.Tools
{
    public class Minimum<T> : Observable<T> where T : IComparable
    {
        /// <summary>
        /// minimum value in range
        /// </summary>
        public Observable<T> Min { get; private set; }

        /// <summary>
        /// ensures the new value is range
        /// </summary>
        /// <param name="value"></param>
        protected override void InternalValidation(ref T value)
        {
            if (value.LessThan(Min)) value = Min;
        }

        /// <summary>
        /// changes the value to be greater than or equal to min value
        /// </summary>
        /// <param name="min"></param>
        protected virtual void ClampToMinValue(T min)
        {
            if (Value.LessThan(min)) Value = Min;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Minimum(T value, T min) : base(value)
        {
            Min = new(min);
            Min.ValueChanged += ClampToMinValue;
        }
    }

    /// <summary>
    /// health value always positive
    /// </summary>
    public sealed class Magnitude : Minimum<double>
    {
        public Magnitude(double value) : base(value, 0)
        {
            
        }
    }

    /// <summary>
    /// stat value that uses positive integers
    /// </summary>
    public sealed class Stat : Minimum<int>
    {
        public Stat(int value) : base(value, 0)
        {
            
        }
    }
}