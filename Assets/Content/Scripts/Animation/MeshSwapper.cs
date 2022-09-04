using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshSwapper : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsForSwap = new List<GameObject>();

    public void SetMesh(GameObject folder)
    {
        var newObj = Instantiate(_objectsForSwap[Random.Range(0, _objectsForSwap.Count - 1)]);
        newObj.transform.position = folder.transform.position;
        newObj.layer = LayerMask.GetMask("Interactable");
        
        Destroy(folder);
    }
}