using System;
using StarterAssets;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static event Action OnDisablePhysics;
    public static event Action OnEnablePhysics;
    private Rigidbody _rigidbody;
    private FirstPersonController _controller;

    private void Start()
    {
        OnDisablePhysics = DisablePhysics;
        OnEnablePhysics = EnablePhysics;
        _rigidbody = GetComponent<Rigidbody>();
        _controller = GetComponent<FirstPersonController>();
    }

    private void EnablePhysics()
    {
        _rigidbody.isKinematic = false;
        _controller.enabled = true;
    }

    private void DisablePhysics()
    {
        _rigidbody.isKinematic = true;
        _controller.enabled = false;
    }
}