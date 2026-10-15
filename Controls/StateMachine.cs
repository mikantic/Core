using System.Collections.Generic;
using UnityEngine;

namespace Core.Controls
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected List<Adapter> adapters { get; } = new();

        public virtual bool CanPerform(Adapter adapter)
        {
            if (adapters.Contains(adapter)) return false;
            return true;
        }

        public virtual void Add(Adapter adapter)
        {
            adapters.Add(adapter);
        }

        public virtual void Complete(Adapter adapter)
        {
            if (!adapters.Contains(adapter)) return;
            adapters.Remove(adapter);
        }

        protected virtual void Iterate()
        {
            for (int i = adapters.Count - 1; i >= 0; i--)
            {
                adapters[i].Iterate();
            }
        }
    }
}