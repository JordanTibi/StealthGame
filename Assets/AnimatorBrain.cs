using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AnimatorBrain : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [SerializeField, Foldout("Actions")] EntityMovement _movement;

    [SerializeField, Foldout("Params"), AnimatorParam(nameof(_animator))] string _speedNameFloat;
    [SerializeField, Foldout("Params"), AnimatorParam(nameof(_animator))] string _xDirectionNameFloat;
    [SerializeField, Foldout("Params"), AnimatorParam(nameof(_animator))] string _zDirectionNameFloat;

    [SerializeField, Foldout("Events"), AnimatorParam(nameof(_animator))] string _Event1;
    [SerializeField, Foldout("Events"), AnimatorParam(nameof(_animator))] string _Event2;

    void Start()
    {
        _movement.OnMove += MovementInjection;
    }

    private void MovementInjection(Vector3 arg0)
    {
        // Create a vector from X and Z
        var fakeDirection = new Vector3(arg0.x, 0, arg0.z);

        // Use that for speed
        if (fakeDirection.magnitude > 0.005f)
        {
            _animator.SetFloat(_speedNameFloat, 1f);
        }
        else
        {
            _animator.SetFloat(_speedNameFloat, 0f);
        }

        // and normalize vector and inject to X and Y animator
        fakeDirection.Normalize();
        _animator.SetFloat(_xDirectionNameFloat, fakeDirection.x);
        _animator.SetFloat(_zDirectionNameFloat, fakeDirection.z);

    }
}
