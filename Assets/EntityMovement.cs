using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] float _speed;
    [SerializeField] float _speedRun;

    Vector3 _direction;
    bool _isRunning;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">La direction souhaité</param>
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }



    public void SetRunning(bool state)
    {
        _isRunning = state;
    }

    void FixedUpdate()
    {
        // Calcul la direction du déplacement
        var calculatedDirection = (_direction * _speed * Time.fixedDeltaTime);
        if (_isRunning)
        {
            calculatedDirection *= _speedRun;
        }

        // Animator
        if (calculatedDirection.magnitude > 0.01f)      // Ya un deplacement en cours
        {
            if (_isRunning)
            {
                _animator.SetFloat("Speed", 10);
            }
            else
            {
                _animator.SetFloat("Speed", 5);
            }
        }
        else  // On ne bouge pas
        {
            _animator.SetFloat("Speed", 0);
        }

        if (calculatedDirection.magnitude > 0.01f)
        {
            if (calculatedDirection.x > 0)
            {
                _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                _rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        _rb.MovePosition(transform.position + calculatedDirection);

    }

    #region EDITOR
#if UNITY_EDITOR
    private void Reset()
    {
        _speed = 5f;
        _speedRun = 1f;
    }
#endif
    #endregion
}
