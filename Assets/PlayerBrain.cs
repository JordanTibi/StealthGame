using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _sprint;
    [SerializeField] InputActionReference _attack;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    //[SerializeField] AttackHitBox _hitbox;

    [SerializeField] float _speed;

    Vector3 _direction;
    Vector3 _aimDirection;
    bool _isRunning;
    bool _isAttack;

    private void Start()
    {
        _move.action.started += StartMove;
        _move.action.performed += UpdateMove;
        _move.action.canceled += StopMove;

        _sprint.action.started += StartSprint;
        _sprint.action.canceled += StopSprint;

        _attack.action.started += StartAttack;
    }

    private void StopSprint(InputAction.CallbackContext obj)
    {
        _isRunning = false;
    }

    private void StartSprint(InputAction.CallbackContext obj)
    {
        _isRunning = true;
    }

    private void FixedUpdate()
    {
        //Debug.Log($"{_direction}");
        _animator.SetBool("IsMoving", _direction.magnitude > 0.1f);
        _animator.SetBool("IsRunning", _isRunning);


        // Move
        if (_isRunning)
        {
            _rb.MovePosition(_rb.transform.position + (_direction * Time.fixedDeltaTime * _speed * 2));
            _animator.SetFloat("Speed", 10);
        }
        else
        {
            _rb.MovePosition(_rb.transform.position + (_direction * Time.fixedDeltaTime * _speed));
            if (_direction.magnitude < 0.05)
            {
                _animator.SetFloat("Speed", 0);
            }
            else
            {
                _animator.SetFloat("Speed", 5);
            }
        }

        // Rotation
        if (_direction.magnitude < 0.01f)
        {
            // Nothing
        }
        else if (_direction.x < 0)
        {
            _rb.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector2>();
        _aimDirection = _direction;
    }
    private void UpdateMove(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector2>();
        _aimDirection = _direction;
    }
    private void StopMove(InputAction.CallbackContext obj)
    {
        _direction = Vector3.zero;
    }

    private void StartAttack(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("isAttack");
        //_hitbox.AttackAllCharacters();
    }
}
