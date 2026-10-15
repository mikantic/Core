using System;
using System.Collections.Generic;
using UnityEngine;
#nullable enable

namespace Core.Properties
{
    [Serializable]
    public class Observable<T> : Property<T>
    {
        public event Action<T?>? ValueChanged;
        public Func<T?, T?> ExternalValidate { protected get; set; } = (value) => value;
        public override T? Value
        {
            get => base.Value;
            set
            {
                value = Validate(value);
                if (Equals(_value, value)) return;
                base.Value = value;
                ValueChanged?.Invoke(_value);
            }
        }
        
        protected virtual T? Validate(T? value)
        {
            return value;
        }

        public Observable() : base() { }
        public Observable(T? value)
        {
            Value = value;
        }
        public Observable(Func<T?, T?> externalValidate) : this()
        {
            ExternalValidate = externalValidate;
        }
        public Observable(T? value, Func<T?, T?> externalValidate) : this(value)
        {
            ExternalValidate = externalValidate;
        }
    }

    [Serializable]
    public class ObservableList<T> : PropertyList<T>
    {
        public event Action<int, T?>? ItemAdded;
        public event Action<int, T?>? ItemRemoved;
        public event Action<int, T?>? ItemModified;

        public Func<T?, T?> ExternalValidate { protected get; set; } = (value) => value;

        protected virtual T? Validate(T? value)
        {
            return value;
        }

        public virtual void Add(T? value)
        {
            value = Validate(value);
            value = ExternalValidate(value);
            _values.Add(value);
            ItemAdded?.Invoke(_values.Count - 1, value);
        }

        public virtual void Set(int index, T? value)
        {
            if (_values.Count <= index)
            {
                Add(value);
                return;
            }
            value = Validate(value);
            value = ExternalValidate(value);
            if (Equals(value, _values[index])) return;
            base[index] = value;
            ItemModified?.Invoke(index, value);
        }

        public virtual void Remove(int index)
        {
            if (_values.Count <= index) return;
            T? value = _values[index];
            _values.RemoveAt(index);
            ItemRemoved?.Invoke(index, value);
        }

        public virtual void Remove(T? item)
        {
            if (!_values.Contains(item)) return;
            Remove(_values.IndexOf(item));
        }

        public override T? this[int index]
        {
            get => base[index];
            set => Set(index, value);
        }

        public ObservableList() : base() { }
        public ObservableList(List<T?> value) : base(value) { }
        public ObservableList(Func<T?, T?> externalValidate) : this()
        {
            ExternalValidate = externalValidate;
        }
        public ObservableList(List<T?> value, Func<T?, T?> externalValidate) : this(value)
        {
            ExternalValidate = externalValidate;
        }
    }

    [Serializable]
    public class ObservableMap<TKey, T> : PropertyMap<TKey, T>
    {
        public event Action<TKey, T?>? ItemAdded;
        public event Action<TKey, T?>? ItemRemoved;
        public event Action<TKey, T?>? ItemModified;

        public Func<TKey, T?, T?> ExternalValidate { protected get; set; } = (key, value) => value;

        protected virtual T? Validate(TKey key, T? value)
        {
            return value;
        }

        public virtual void Add(TKey key, T? value)
        {
            value = Validate(key, value);
            _values.Add(key, value);
            ItemAdded?.Invoke(key, value);
        }

        public virtual void Set(TKey key, T? value)
        {
            if (!_values.ContainsKey(key))
            {
                Add(key, value);
                return;
            }
            value = Validate(key, value);
            if (Equals(value, _values[key])) return;
            base[key] = value;
            ItemModified?.Invoke(key, value);
        }

        public virtual void Remove(TKey key)
        {
            if (!_values.ContainsKey(key)) return;
            T? item = _values[key];
            _values.Remove(key);
            ItemRemoved?.Invoke(key, item);
        }

        public override T? this[TKey key]
        {
            get => base[key];
            set => Set(key, value);
        }

        public ObservableMap() : base() { }
        public ObservableMap(Dictionary<TKey, T?> value) : base(value) { }
        public ObservableMap(Func<TKey, T?, T?> externalValidate) : this()
        {
            ExternalValidate = externalValidate;
        }
        public ObservableMap(Dictionary<TKey, T?> value, Func<TKey, T?, T?> externalValidate) : this(value)
        {
            ExternalValidate = externalValidate;
        }
    }
}