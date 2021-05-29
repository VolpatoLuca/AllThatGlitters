using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public bool IsPlayerNear { get; set; }

    private void Update()
    {
        if (canvas != null && canvas.activeSelf != IsPlayerNear)
            canvas.SetActive(IsPlayerNear);
    }
    public abstract void Interact();
}