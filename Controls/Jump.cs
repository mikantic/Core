using Physics;
using Physics.Influences;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Contacts))]
[RequireComponent(typeof(Body))]
public class Jump : IteratedControl
{
    [SerializeField] protected float _magnitude = 20f;
    [SerializeField] protected float _maxDuration = 0.25f;
    [SerializeField] protected float _coyoteDuration = 0.25f;
    protected Contacts _contacts;
    protected Body _body;
    protected bool _doubleJump = false;
    protected float? _performedTime = null;
    protected float? _coyoteTime = null;

    protected override void Awake()
    {
        if (!TryGetComponent(out _contacts) 
        || !TryGetComponent(out _body))
        {
            OnCanceled();
            return;
        }
        _contacts.Influencers.OnRemoved += OnContactRemoved;
        _contacts.Influencers.OnAdded += OnContactAdded;
        base.Awake();
    }

    protected override void OnDestroy()
    {
        _contacts.Influencers.OnRemoved -= OnContactRemoved;
        _contacts.Influencers.OnAdded -= OnContactAdded;
        base.OnDestroy();
    }

    protected virtual void OnContactRemoved<T>(T _)
    {
        Debug.Log("Removed");
        _coyoteTime = _contacts.Influencers.Count <= 0 ? Time.time : null;
        _doubleJump = true;
    }

    protected virtual void OnContactAdded<T>(T _)
    {
        Debug.Log("Added");
    }

    protected override void OnPerformed()
    {
        if (_contacts.Influencers.Count > 0
        || (Time.time - _coyoteTime <= _coyoteDuration))
        {
            _doubleJump = true;
            _performedTime = Time.time;
            base.OnPerformed();   
            return;
        }

        if (_doubleJump)
        {
            _doubleJump = false;
            _performedTime = Time.time;
            base.OnPerformed();
            return;
        }

        OnCanceled();
    }

    protected override void OnCanceled()
    {
        _performedTime = null;
        base.OnCanceled();
    }

    protected virtual void FixedUpdate()
    {
        if (!_performedTime.HasValue
        || (Time.time - _performedTime.Value > _maxDuration))
        {
            OnCanceled();
            return;
        }

        if (_body == null) return;
        _body.AddForce(_contacts.Normal.Value.normalized * _magnitude, _magnitude, ForceType.Local);
    }
}