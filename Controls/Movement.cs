using Physics;
using Physics.Influences;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Contacts))]
[RequireComponent(typeof(Body))]
public class Movement : IteratedControl
{
    [SerializeField] protected Camera _camera;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _accelerationDuration;
    protected Contacts _contacts;
    protected Body _body;

    protected override void Awake()
    {
        if (!TryGetComponent(out _contacts) 
        || !TryGetComponent(out _body))
        {
            OnCanceled();
            return;
        }
        base.Awake();
    }

    protected Vector3 GetDirection(Camera camera, Vector2 direction)
    {
        Vector3 force = (camera.transform.forward * direction.y + camera.transform.right * direction.x).normalized;
        return force.Project(_contacts.Normal);
    }

    protected virtual void FixedUpdate()
    {
        if (_camera == null) return;

        Vector2 input = _input.action.ReadValue<Vector2>();
        Vector3 direction = GetDirection(_camera, input);
        
        float acceleration = _speed / _accelerationDuration;
        _body.AddForce(direction * acceleration * Time.fixedDeltaTime, _speed, ForceType.Local); 
    }
}
