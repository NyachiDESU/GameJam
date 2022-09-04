using UnityEngine;

public class AnimationStateReporter : StateMachineBehaviour
{
    private IAnimationStateReader _reader;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        FindReader(animator);
        
        _reader.EnteredState(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        FindReader(animator);
        
        _reader.ExitedState(stateInfo.shortNameHash);
    }
    
    private void FindReader(Animator animator)
    {
        if (_reader == null)
            _reader = animator.GetComponent<IAnimationStateReader>();
    }
}