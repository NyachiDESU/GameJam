using System.Collections;
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
            StartCoroutine(WaitForClip());
        }

        private IEnumerator WaitForClip()
        {
            yield return new WaitForSeconds(0.65f);
            AudioManager.PlayClip(ClipType.KeyUse, _lookPoint.transform.position, 0.9f);
            yield return new WaitForSeconds(0.25f);
            AudioManager.PlayClip(ClipType.KeyUse, _lookPoint.transform.position, 0.9f);
        }
    }
}
