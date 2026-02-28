using System;

namespace Core.Flags
{
    public struct Flag<T> where T : Enum
    {
        private T _flags;

        public Flag(T value)
        {
            _flags = value;
        }

        public T Flags => _flags;

        public void Set(T flag)
        {
            _flags = (T)Enum.ToObject(typeof(T), Convert.ToUInt64(_flags) | Convert.ToUInt64(flag));
        }

        public void Remove(T flag)
        {
            _flags = (T)Enum.ToObject(typeof(T), Convert.ToUInt64(_flags) & ~Convert.ToUInt64(flag));
        }

        public bool Has(T flag)
        {
            return (Convert.ToUInt64(_flags) & Convert.ToUInt64(flag)) == Convert.ToUInt64(flag);
        }
    }    
}

