using UnityEngine;
using UnityEngine.InputSystem;

public abstract class IteratedControl : MonoBehaviour
{
    [SerializeField] protected InputActionReference _input;
    
    protected virtual void Awake()
    {
        _input.action.performed += OnPerformed;
        _input.action.canceled += OnCanceled;
        OnCanceled();
    }
    protected virtual void OnPerformed()
    {
        enabled = true;
    }
    protected virtual void OnPerformed<T>(T _) => OnPerformed();

    protected virtual void OnCanceled()
    {
        enabled = false;
    }
    protected virtual void OnCanceled<T>(T _) => OnCanceled();

    protected virtual void OnDestroy()
    {
        _input.action.performed -= OnPerformed;
        _input.action.canceled -= OnCanceled;
    }
}