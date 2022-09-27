using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _sprintInput;
    [SerializeField] InputActionReference _JumpImput;

    [Header("Actions")]
    [SerializeField] EntityMovement _movement;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;

    Vector3 _direction;
    Vector3 _aimDirection;
    bool _isRunning;
    bool _isAttack;
    bool _isMoving;

    private void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += MoveStop;

        _sprintInput.action.started += StartSprint;
        _sprintInput.action.canceled += StopSprint;

    }
    private void OnDestroy()
    {
        _moveInput.action.started -= StartMove;
        _moveInput.action.performed -= UpdateMove;
        _moveInput.action.canceled -= MoveStop;
    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        var keyboard = obj.ReadValue<Vector2>();
        _movement.Direction = new Vector3(keyboard.x, 0, keyboard.y);
    }

    private void UpdateMove(InputAction.CallbackContext obj)
    {
        var keyboard = obj.ReadValue<Vector2>();
        _movement.Direction = new Vector3(keyboard.x, 0, keyboard.y);
    }

    private void MoveStop(InputAction.CallbackContext obj)
    {
        _movement.Direction = Vector3.zero;

    }

    private void StopSprint(InputAction.CallbackContext obj)
    {
        _isRunning = false;
    }

    private void StartSprint(InputAction.CallbackContext obj)
    {
        _isRunning = true;
    }

    
}
