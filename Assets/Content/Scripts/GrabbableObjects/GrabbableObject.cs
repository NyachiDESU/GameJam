using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _objectTransformPoint;
    private float _lerpSpeed = 10;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectTransform)
    {
        _objectTransformPoint = objectTransform;
        _rigidbody.useGravity = false; 
        _rigidbody.isKinematic = true; 
        
    }

    public void Drop()
    {
        _objectTransformPoint = null;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false; 
    }

    private void FixedUpdate()
    {
        if (_objectTransformPoint != null)
        {
            var newPos = Vector3.Lerp(transform.position, _objectTransformPoint.position, Time.fixedDeltaTime * _lerpSpeed);
            _rigidbody.MovePosition(newPos);
            
            //TODO: use in usage animation (rotate object to player camera)
            /*var lookDir = transform.position - _objectTransformPoint.position;
            lookDir.Normalize();
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 5 * Time.fixedDeltaTime);*/
        }
    }
}