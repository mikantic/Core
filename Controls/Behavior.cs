using System;
using UnityEngine;
namespace Core.Controls
{
    public abstract class Behavior : MonoBehaviour
    {
        public abstract bool TryPerform();
        public abstract void Iterate();
        public virtual bool TryCancel()
        {
            Complete();
            return true;
        }
        public event Action OnComplete;
        public virtual void Complete() => OnComplete?.Invoke();
    }

    public abstract class Behavior<T> : Behavior
    {
        public abstract bool TryPerform(T value);
        public abstract void Iterate(T value);
        public sealed override void Iterate() { }
        public sealed override bool TryPerform() => false;
    }

}