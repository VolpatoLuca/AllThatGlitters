using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DiscoverableItem : MonoBehaviour
{
    public bool IsDiscovered { get; private set; }

    [SerializeField] private List<MeshRenderer> renderers = new List<MeshRenderer>();

    private void Start()
    {
        foreach (var meshR in transform.GetComponentsInChildren<MeshRenderer>())
        {
            renderers.Add(meshR);
        }
        if (TryGetComponent(out MeshRenderer mr))
            renderers.Add(mr);
        foreach (var r in renderers)
        {
            r.enabled = false;
        }
        //DiscoverItem();
        //Debug.Log("Ricorda di levare discover");

    }

    public void DiscoverItem()
    {
        if (IsDiscovered)
            return;
        IsDiscovered = true;
        foreach (var r in renderers)
        {
            r.enabled = true;
        }
    }
}
