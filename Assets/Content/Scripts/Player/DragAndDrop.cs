using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Transform _objectTransformPoint;
    private Transform _cameraTransform;
    private GrabbableObject _grabbableObject;
    private InteractableObject _highlightObject;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GrabObject();
        }
        else
        {
            if (!_grabbableObject)
            {
                SelectObject();
            }
        }
    }

    private bool ObjectChecked(out RaycastHit hit)
    {
        var pickupDist = 2f;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit1,
            pickupDist))
        {
            hit = hit1;
            return true;
        }

        hit = hit1;
        return false;
    }

    private void GrabObject()
    {
        if (ObjectChecked(out RaycastHit hit))
        {
            if (_grabbableObject == null)
            {
                if (hit.transform.TryGetComponent(out _grabbableObject) && _grabbableObject.IsGrabbable)
                    _grabbableObject.Grab(_objectTransformPoint);
                else if (hit.transform.TryGetComponent(out _grabbableObject) && !_grabbableObject.IsGrabbable)
                {
                    _grabbableObject.StartAnimation();
                }
            }
            else
            {
                _grabbableObject.Drop();
                _grabbableObject = null;
            }
        }
    }

    private void SelectObject()
    {
        if (ObjectChecked(out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out InteractableObject interactableObject))
            {
                if (!interactableObject)
                {
                    ResetHighlight();
                    _highlightObject = null;
                }
                else
                {
                    interactableObject.Highlight();
                    if (_highlightObject != interactableObject)
                    {
                        ResetHighlight();
                        _highlightObject = interactableObject;
                    }
                }
            }
        }
        else
        {
            ResetHighlight();
        }
    }

    private void ResetHighlight()
    {
        if (_highlightObject) _highlightObject.Reset();
    }
}