using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Controls
{
    public abstract class Adapter
    {
        protected virtual Behavior behavior { get; }
        protected readonly StateMachine stateMachine;

        public Adapter(
            Behavior behavior,
            StateMachine stateMachine
        )
        {
            this.behavior = behavior;
            this.stateMachine = stateMachine;
            this.behavior.OnComplete += OnComplete;
        }

        public virtual void Perform()
        {
            if (!stateMachine.CanPerform(this)) return;
            if (!behavior.TryPerform()) return;
            stateMachine.Add(this);
        }
        public virtual void Iterate() => behavior.Iterate();
        public virtual void Complete() => behavior.Complete();
        public virtual bool TryCancel() => behavior.TryCancel();
        protected virtual void OnComplete() => stateMachine.Complete(this);
    }

    public class EmptyAdapter : Adapter
    {
        public EmptyAdapter(
            Behavior behavior,
            StateMachine stateMachine
        ) : base(behavior, stateMachine) { }

        public virtual void Perform<T>(T _) => Perform();
        public virtual void Complete<T>(T _) => Complete();
    }

    public abstract class Adapter<T> : Adapter where T : struct
    {
        protected readonly Behavior<T> typedBehavior;
        public Func<T> ReadValue { protected get; set; }

        public Adapter(
            Behavior<T> behavior,
            StateMachine stateMachine
        ) : base(behavior, stateMachine)
        {
            typedBehavior = behavior;
        }

        public virtual void Perform(T value)
        {
            if (!stateMachine.CanPerform(this)) return;
            if (!typedBehavior.TryPerform(value)) return;
            stateMachine.Add(this);
        }

        public override void Iterate()
        {
            if (ReadValue == null) return;
            typedBehavior.Iterate(ReadValue());
        }

        public virtual void Complete(T value) => Complete();
    }


    public abstract class InputActionAdapter<T1, T2> : Adapter where T2 : struct where T1 : struct
    {
        protected readonly Behavior<T2> typedBehavior;
        public Func<T1> ReadValue { protected get; set; }

        protected abstract T2 Adapt(T1 value);

        public InputActionAdapter(
            Behavior<T2> behavior,
            StateMachine stateMachine
        ) : base(behavior, stateMachine)
        {
            typedBehavior = behavior;
        }

        public virtual void Perform(InputAction.CallbackContext value)
        {
            if (!stateMachine.CanPerform(this)) return;
            if (!typedBehavior.TryPerform(Adapt(value.ReadValue<T1>()))) return;
            stateMachine.Add(this);
        }

        public override void Iterate()
        {
            if (ReadValue == null) return;
            typedBehavior.Iterate(Adapt(ReadValue()));
        }

        public virtual void Complete(InputAction.CallbackContext value) => Complete();
    }



    public static class AdaptorExtensions
    {        
        public static void Link<T>(this Adapter adapter, ComputerAction computerAction)
        {
            computerAction.Performed += adapter.Perform;
            computerAction.Canceled += adapter.Complete;
        }

        public static void UnLink<T>(this Adapter adapter, ComputerAction computerAction)
        {
            computerAction.Performed -= adapter.Perform;
            computerAction.Canceled -= adapter.Complete;
        }



        public static void Link(this EmptyAdapter adapter, InputAction inputAction)
        {
            inputAction.performed += adapter.Perform;
            inputAction.canceled += adapter.Complete;
        }

        public static void UnLink(this EmptyAdapter adapter, InputAction inputAction)
        {
            inputAction.performed -= adapter.Perform;
            inputAction.canceled -= adapter.Complete;
        }

        public static void Link<T>(this EmptyAdapter adapter, ComputerAction<T> computerAction)
        {
            computerAction.Performed += adapter.Perform;
            computerAction.Canceled += adapter.Complete;
        }

        public static void UnLink<T>(this EmptyAdapter adapter, ComputerAction<T> computerAction)
        {
            computerAction.Performed -= adapter.Perform;
            computerAction.Canceled -= adapter.Complete;
        }


        public static void Link<T>(this Adapter<T> adapter, ComputerAction<T> computerAction) 
        where T : struct
        {
            computerAction.Performed += adapter.Perform;
            adapter.ReadValue = computerAction.ReadValue;
            computerAction.Canceled += adapter.Complete;
        }

        public static void UnLink<T>(this Adapter<T> adapter, ComputerAction<T> computerAction) 
        where T : struct
        {
            computerAction.Performed -= adapter.Perform;
            adapter.ReadValue = null;
            computerAction.Canceled -= adapter.Complete;
        }


        public static void Link<T1, T2>(this InputActionAdapter<T1, T2> adapter, InputAction inputAction) 
        where T1 : struct
        where T2 : struct
        {
            inputAction.performed += adapter.Perform;
            adapter.ReadValue = inputAction.ReadValue<T1>;
            inputAction.canceled += adapter.Complete;
        }

        public static void UnLink<T1, T2>(this InputActionAdapter<T1, T2> adapter, InputAction inputAction) 
        where T1 : struct
        where T2 : struct
        {
            inputAction.performed -= adapter.Perform;
            adapter.ReadValue = null;
            inputAction.canceled -= adapter.Complete;
        }


    }
}