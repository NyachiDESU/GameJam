using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    private Rigidbody _rigidbody;
    private Transform _objectTransformPoint;
    private Transform _face;
    private static Transform _cameraTransform;
    private float _lerpSpeed = 10;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectTransform, Transform face)
    {
        _objectTransformPoint = objectTransform;
        _face = face;
        _rigidbody.useGravity = false; 
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        _objectTransformPoint = null;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false; 
    }

    private void Update()
    {
        if (_objectTransformPoint != null)
        {
            var _direction = (_face.position - transform.position).normalized;//CameraTransform.position - 
            var _lookRotation = Quaternion.LookRotation(-_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (_objectTransformPoint != null)
        {
            var newPos = Vector3.Lerp(transform.position, _objectTransformPoint.position, Time.fixedDeltaTime * _lerpSpeed);
            
            _rigidbody.MovePosition(newPos);
        }
    }
}