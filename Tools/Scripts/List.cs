using System;
using System.Collections.Generic;

namespace Core.Tools
{
    public class List<T> : Hidden<System.Collections.Generic.List<T>>
    {
        public IReadOnlyList<T> Items => _value;

        public event Action ItemsModified;
        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;

        public List()
        {
            _value = new();
        }

        protected virtual bool TryAdd(T item)
        {
            if (item == null) return false;
            _value.Add(item);
            return true;
        }

        protected virtual void AddResponse(T item)
        {
            ItemAdded?.Invoke(item);
            ItemsModified?.Invoke();
        }

        public void Add(T item)
        {
            if (!TryAdd(item)) return;
            AddResponse(item);
        }

        protected virtual bool TryRemove(T item)
        {
            if (item == null) return false;
            _value.Remove(item);
            return true;
        }

        protected virtual void RemoveResponse(T item)
        {
            ItemRemoved?.Invoke(item);
            ItemsModified?.Invoke();
        }

        public void Remove(T item)
        {
            if (!TryRemove(item)) return;
            RemoveResponse(item);
        }
    }

    public class Set<T> : List<T>
    {
        protected override bool TryAdd(T item)
        {
            if (_value.Contains(item)) return false;
            return base.TryAdd(item);
        }
    }
}