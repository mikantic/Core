using System;
using UnityEngine;
#nullable enable

namespace Core.Properties
{
    [Serializable]
    public class Range<T> : Observable<T> where T : IComparable
    {
        [SerializeField] public Observable<T> Minimum;
        [SerializeField] public Observable<T> Maximum;

        public Range(T? minimum, T? maximum) : base()
        {
            Minimum = new(minimum);
            Maximum = new(maximum);

            Minimum.ValueChanged += RecalculateValue;
            Maximum.ValueChanged += RecalculateValue;
        }

        public Range(T value, T? minimum, T? maximum) : this(minimum, maximum)
        {
            Value = value;
        }

        protected void RecalculateValue() => Value = Validate(Value);
        protected void RecalculateValue(T? _) => RecalculateValue();

        protected override T? Validate(T? value)
        {
            if (Minimum != null && (value?.CompareTo(Minimum) ?? 0) < 0) return Minimum;
            if (Maximum != null && (value?.CompareTo(Maximum) ?? 0) > 0) return Maximum;
            return base.Validate(value);
        }
    }

    public class Positive<T> : Observable<T> where T : IComparable
    {
        public Positive(T? value) : base(value) { }
        public Positive() : base() { }

        protected override T? Validate(T? value)
        {
            if ((value?.CompareTo(default) ?? 0) < 0) value = default;
            return base.Validate(value);
        }
    }
}