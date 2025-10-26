using System;

namespace Core.Tools
{
    [Serializable]
    public class Observable<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                value = PreValueChanged(value);
                if (_value.Equals(value)) return;
                _value = value;
                PostValueChanged();
            }
        }

        public void SetValue(T value) => Value = value;

        public Func<T, T> Validation { get; set; }
        public event Action<T> OnValueChanged;

        protected virtual T PreValueChanged(T value)
        {
            if (Validation != null) return Validation(value);
            return value;
        }

        protected virtual void PostValueChanged()
        {
            OnValueChanged?.Invoke(_value);
        }
    }    

}