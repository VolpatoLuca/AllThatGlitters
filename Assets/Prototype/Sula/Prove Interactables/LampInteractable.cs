using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]

public class LampInteractable : Interactable
{
    SphereCollider trigger;
    public bool playerVicino;
    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;       
    }

    private void Update()
    {
        playerVicino = isPlayerNear;
    }
}
