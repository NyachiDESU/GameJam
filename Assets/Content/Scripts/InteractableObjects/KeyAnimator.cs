using System;
using UnityEngine;

namespace GameJam
{
    public class KeyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int _useKeyHash = Animator.StringToHash("UseKey");
    
        public Action<AnimatorState> OnStateExited;
    
        private AnimatorState _state;
    
        private void Awake()
        {
            _animator.enabled = false;
        }
    
        public void EnteredState(int state)
        {
            _state = StateFor(state);
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
            
            if (stateHash == _useKeyHash)
                state = AnimatorState.Get;
            else
                state = AnimatorState.Unknown;
            
            return state;
        }
    
        public void PlayUseKey()
        {
            _animator.enabled = true;
            _animator.SetTrigger(_useKeyHash);
        }
    
        // public void OnUsedKey()
        // {
        //     Debug.Log("Key has been used");
        //     _animator.enabled = false;
        //     _particleSystem.SetActive(true);
        // }
    }
}