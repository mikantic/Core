using System;
using UnityEngine;

namespace Core.Tools
{
    [Serializable]
    public abstract class Range<T> : Observable<T> where T : IComparable
    {
        /// <summary>
        /// minimum value in range
        /// </summary>
        [SerializeField] public Observable<T> Min { get; private set; }

        /// <summary>
        /// maximum value in range
        /// </summary>
        [SerializeField] public Observable<T> Max { get; private set; }

        /// <summary>
        /// implement getting the inverse lerp of the value
        /// </summary>
        public abstract T InverseLerp { get; }

        /// <summary>
        /// implement getting distance from min to max
        /// </summary>
        public abstract T Distance { get; }

        /// <summary>
        /// ensures the new value is range
        /// </summary>
        /// <param name="value"></param>
        protected override void InternalValidation(ref T value)
        {
            if (value.LessThan(Min)) value = Min;
            if (value.GreaterThan(Max)) value = Max;
        }

        /// <summary>
        /// prevents min value being set above max
        /// </summary>
        /// <param name="min"></param>
        protected virtual void ValidateMin(ref T min)
        {
            if (min.GreaterThan(Max)) min = Max;
        }

        /// <summary>
        /// prevents max value being set below min
        /// </summary>
        /// <param name="max"></param>
        protected virtual void ValidateMax(ref T max)
        {
            if (max.LessThan(Min)) max = Min;
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
        /// changes the value to be less than or equal to max value
        /// </summary>
        /// <param name="max"></param>
        protected virtual void ClampToMaxValue(T max)
        {
            if (Value.GreaterThan(max)) Value = max;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Range(T value, T min, T max) : base(value)
        {
            if (min.GreaterThan(max)) Utils.SwapWith(ref min, ref max);

            Min = new(min, ValidateMin);
            Min.ValueChanged += ClampToMinValue;

            Max = new(max, ValidateMax);
            Max.ValueChanged += ClampToMaxValue;
        }
    }

    [Serializable]
    public class Range : Range<double>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Range(double value, double min, double max) : base(value, min, max) { }

        /// <summary>
        /// lerp percentage
        /// </summary>
        public override double InverseLerp => Value.InverseLerp(Min, Max);

        /// <summary>
        /// distance from min to max
        /// </summary>
        public override double Distance => Max - Min;
    }

    public class FloatRange : Range<float>
    {
        public FloatRange(float value, float min, float max) : base(value, min, max) {}

        /// <summary>
        /// lerp percentage
        /// </summary>
        public override float InverseLerp => Mathf.InverseLerp(Min, Max, Value);

        /// <summary>
        /// distance from min to max
        /// </summary>
        public override float Distance => Max - Min;
    }

    /// <summary>
    /// range from 0 to 1
    /// </summary>
    public class Ratio : Range
    {
        public Ratio(double value) : base(value, 0, 1) { }
    }

    /// <summary>
    /// range from 0 to 100
    /// </summary>
    public class Meter : Range
    {
        public Meter(double value) : base(value, 0, 100) { }
    }
}