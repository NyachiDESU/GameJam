using System;
using UnityEngine;

public class FolderAnimator : MonoBehaviour, IAnimationStateReader
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _particleSystem;
    
    private static readonly int _getFolderHash = Animator.StringToHash("GetFolder");
    private static readonly int _openFolderHash = Animator.StringToHash("OpenFolder");

    public Action<AnimatorState> OnStateEntered;
    public Action<AnimatorState> OnStateExited;

    private AnimatorState _state;
    
    public bool IsGotten;
    public bool IsOpened;

    private void Awake()
    {
        _animator.enabled = false;
    }

    public void EnteredState(int state)
    {
        _state = StateFor(state);
        OnStateEntered?.Invoke(_state);
    }
    
    public void ExitedState(int state)
    {
        _state = StateFor(state);
        OnStateExited?.Invoke(_state);
        _animator.enabled = false;
    }

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        
        if (stateHash == _getFolderHash)
            state = AnimatorState.Get;
        else if (stateHash == _openFolderHash)
            state = AnimatorState.Open;
        else
            state = AnimatorState.Unknown;
        
        return state;
    }

    public void PlayGetBook()
    {
        _animator.enabled = true;
        IsGotten = true;
        _animator.SetTrigger(_getFolderHash);
    }
    
    public void PlayOpenBook()
    {
        _animator.enabled = true;
        IsOpened = true;
        _animator.SetTrigger(_openFolderHash);
    }


    public void Disable()
    {
        //_animator.enabled = false;
    }

    public void OnOpened()
    {
        Debug.Log("OnOpened");
        _animator.enabled = false;
        _particleSystem.SetActive(true);
    }
}

public enum AnimatorState
{
    Get,
    Open,
    Unknown
}