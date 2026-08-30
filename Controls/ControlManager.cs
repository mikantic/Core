using Physics;
using Physics.Influences;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _movement;
    private void Awake()
    {
        _movement.Enable();
    }
}