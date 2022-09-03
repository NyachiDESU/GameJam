using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void Highlight()
    {
        _outline.enabled = true;
    }

    public void Reset()
    {
        _outline.enabled = false;
    }
}