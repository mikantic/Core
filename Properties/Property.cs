using System;
using System.Collections.Generic;
using UnityEngine;
#nullable enable

namespace Core.Properties
{
    [Serializable]
    public class Property<T>
    {
        [SerializeField] protected T? _value;
        public virtual T? Value
        {
            get => _value;
            set => _value = value;
        }

        public static implicit operator T?(Property<T> property) => property.Value;
        public override string ToString() => Value?.ToString() ?? "null";

        public Property()
        {
            
        }

        public Property(T? value) : this()
        {
            Value = value;
        }
    }

    [Serializable]
    public class PropertyList<T>
    {
        [SerializeField] protected List<T?> _values = new();
        public virtual IReadOnlyList<T?> Values => _values;

        public virtual T? this[int index]
        {
            get => Values[index];
            set => _values[index] = value;
        }

        public PropertyList()
        {
            
        }

        public PropertyList(List<T?> values) : this()
        {
            _values = values;
        }
    }
    
    [Serializable]
    public class PropertyMap<TKey, T>
    {
        [SerializeField] protected Dictionary<TKey, T?> _values = new();
        public virtual IReadOnlyDictionary<TKey, T?> Values => _values;

        public virtual T? this[TKey key]
        {
            get => Values[key];
            set => _values[key] = value;
        }

        public int Count => Values.Count;

        public PropertyMap()
        {
            
        }

        public PropertyMap(Dictionary<TKey, T?> values) : this()
        {
            _values = values;
        }
    }
}