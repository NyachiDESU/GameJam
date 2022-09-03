using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Transform _objectTransformPoint;
    private Transform _cameraTransform;
    private GrabbableObject _grabbableObject;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_grabbableObject == null)
            {
                var pickupDist = 2f;
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit,
                    pickupDist))
                {
                    if (hit.transform.TryGetComponent(out _grabbableObject))
                    {
                        _grabbableObject.Grab(_objectTransformPoint);
                    }
                }
            }
            else
            {
                _grabbableObject.Drop();
                _grabbableObject = null; 
            }
        }
    }
}