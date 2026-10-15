using System;
using UnityEngine;

namespace Core.Controls
{
    public abstract class ComputerAction : MonoBehaviour
    {
        public event Action Performed;
        public event Action Canceled;
    }

    public abstract class ComputerAction<T> : MonoBehaviour
    {
        public event Action<T> Performed;
        public event Action Canceled;
        public abstract T ReadValue();
    }
}