using System;
using System.Collections.Generic;

namespace Core.Tools
{   
    public class Map<TKey, TValue> : Hidden<Dictionary<TKey, TValue>>
    {
        public IReadOnlyDictionary<TKey, TValue> Data => _value;

        public Func<TValue, bool> Validation;

        public Map()
        {
            _value = new();
        }

        public void Set(TKey key, TValue value)
        {
            if (_value.ContainsKey(key) || (Validation?.Invoke(value) ?? true)) _value[key] = value;   
        }

        public void Remove(TKey key)
        {
            _value.Remove(key);
        }

        public TValue this[TKey key]
        {
            get => Data[key];
            set => Set(key, value);
        }
    }


    // public struct EnumMap<TEnum, TValue>
    // where TEnum : Enum
    // {
    //     private TValue[] _map;

    //     public EnumMap(params (TEnum key, TValue value)[] presets)
    //     {
    //         int length = Enum.GetValues(typeof(TEnum)).Length;
    //         _map = new TValue[length];

    //         for (int i = 0; i < length; i++)
    //             _map[i] = default;

    //         if (presets == null) return;

    //         foreach (var (key, value) in presets)
    //             _map[Convert.ToInt32(key)] = value;
    //     }

    //     public TValue this[TEnum key]
    //     {
    //         get => _map[Convert.ToInt32(key)];
    //         set => _map[Convert.ToInt32(key)] = value;
    //     }
    // }


}