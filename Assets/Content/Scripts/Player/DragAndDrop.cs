using GameJam;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Transform _objectTransformPoint;
    [SerializeField] private Transform _facePoint;
    private Transform _cameraTransform;
    private GrabbableObject _grabbableObject;
    private InteractableObject _highlightObject;

    private KeyController key;


    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AnimateObject();
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

    private KeyController CheckForKey()
    {
        if (_grabbableObject && _grabbableObject.transform.CompareTag("Key"))
            key = _grabbableObject.GetComponent<KeyController>();
        else
            key = null;

        return key;
    }

    private bool CheckForDoor()
    {
        var pickupDist = 5f;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit1, pickupDist))
        {
            if (hit1.transform.CompareTag("Door"))
                return true;
        }

        return false;
    }

    private void OpenDoor()
    {
        key.UseKey();
        ResetHighlight();
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
                if (hit.transform.TryGetComponent(out _grabbableObject) && _grabbableObject.enabled)
                    _grabbableObject.Grab(_objectTransformPoint, _facePoint);
            }
            else
            {
                if (CheckForKey())
                {
                    if (CheckForDoor())
                    {
                        OpenDoor();
                        return;
                    }
                }
                
                _grabbableObject.Drop();
                _grabbableObject = null;
            }
        }
    }

    private void AnimateObject()
    {
        if (ObjectChecked(out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Folder"))
            {
                var animator = hit.collider.GetComponent<FolderAnimator>();

                if (!animator.IsGotten)
                    animator.PlayGetBook();
                else if(!animator.IsOpened)
                    animator.PlayOpenBook();
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