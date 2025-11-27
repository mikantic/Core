using System;
using Unity.VisualScripting;

namespace Core.Tools
{
    public abstract class Range<T> : Observable<T> where T : IComparable
    {
        public Observable<T> Min { get; private set; }
        public Observable<T> Max { get; private set; }

        public abstract T Lerp { get; }

        protected override void InternalValidation(ref T value)
        {
            if (value.LessThan(Min)) value = Min;
            if (value.GreaterThan(Max)) value = Max;
        }

        protected virtual void ValidateMin(ref T min)
        {
            if (min.GreaterThan(Max)) min = Max;
        }

        protected virtual void ValidateMax(ref T max)
        {
            if (max.LessThan(Min)) max = Min;
        }

        protected virtual void ClampToMinValue(T min)
        {
            if (Value.LessThan(min)) Value = Min;
        }

        protected virtual void ClampToMaxValue(T max)
        {
            if (Value.GreaterThan(max)) Value = max;
        }

        public Range(T value, T min, T max) : base(value)
        {
            if (min.GreaterThan(max)) Utils.SwapWith(ref min, ref max);

            Min = new(min, ValidateMin);
            Min.ValueChanged += ClampToMinValue;

            Max = new(max, ValidateMax);
            Max.ValueChanged += ClampToMaxValue;
        }
    }

    /// <summary>
    /// default class uses doubles
    /// </summary>
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
        public override double Lerp => (Value - Min) / (Max - Min);
    }
}