using System;

namespace Core.Tools
{
    public class Observable<T> : Property<T>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        public Observable(T value, Ref<T> validation = null) : base(value)
        {
            if (validation != null) ExternalValidation = validation;
        }

        /// <summary>
        /// event for externally validating changes
        /// </summary>
        public event Ref<T> ExternalValidation;

        /// <summary>
        /// method to interally validate changes
        /// </summary>
        /// <param name="value"></param>
        protected virtual void InternalValidation(ref T value) { }

        /// <summary>
        /// event for value changing
        /// </summary>
        public event Action<T> ValueChanged;

        /// <summary>
        /// response to a change in value internally
        /// </summary>
        protected virtual void InternalResponse()
        {
            ValueChanged?.Invoke(Value);
        }

        /// <summary>
        /// applies validation to value and responses
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool TrySetValue(T value)
        {
            InternalValidation(ref value);
            ExternalValidation?.Invoke(ref value);
            if (!base.TrySetValue(value)) return false;
            InternalResponse();
            return true;
        }

        /// <summary>
        /// access to value implicitly
        /// </summary>
        /// <param name="observable"></param>
        public static implicit operator T(Observable<T> observable) => observable.Value;
    }

}