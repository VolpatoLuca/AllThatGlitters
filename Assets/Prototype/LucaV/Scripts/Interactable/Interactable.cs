using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public bool IsPlayerNear { get; set; }

    private void Start()
    {
        Vector3 rot = Vector3.zero;
        rot.x = 30;
        if (canvas)
            canvas.transform.eulerAngles = rot;
    }

    protected virtual void Update()
    {
        if (canvas != null && canvas.activeSelf != IsPlayerNear)
            canvas.SetActive(IsPlayerNear);
    }
    public abstract void Interact();
}