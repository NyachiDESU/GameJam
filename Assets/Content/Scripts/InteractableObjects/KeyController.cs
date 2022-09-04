using UnityEngine;

namespace GameJam
{
    public class KeyController : MonoBehaviour
    {
        [SerializeField] private KeyAnimator _animator;
        [SerializeField] private GrabbableObject _grabbableObject;
        [SerializeField] private Transform _animationPoint;
        [SerializeField] private Transform _lookPoint;
        [SerializeField] private Rigidbody _rigidbody;

    
        public void UseKey()
        {
            _rigidbody.isKinematic = true;
            _grabbableObject.enabled = false;
            transform.position = _animationPoint.transform.position;
            transform.rotation = Quaternion.identity;
            transform.LookAt(_lookPoint);
            _animator.PlayUseKey();
        }
    }
}
