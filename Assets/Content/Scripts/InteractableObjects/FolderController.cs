using System;
using UnityEngine;

public class FolderController : MonoBehaviour
{
    [SerializeField] private FolderAnimator _animator;
    [SerializeField] private GrabbableObject _grabbableObject;
    

    private void Start()
    {
        _animator.OnStateExited += AnimationExited;
    }
    

    private void AnimationExited(AnimatorState state)
    {
        Debug.Log($"Gotten state: {state}");
        if (state == AnimatorState.Get)
        {
            _grabbableObject.enabled = true;
            _animator.IsGotten = true;
        }
        else if (state == AnimatorState.Open)
        {
            _animator.IsOpened = true;
            _animator.Disable();
        }
    }
}