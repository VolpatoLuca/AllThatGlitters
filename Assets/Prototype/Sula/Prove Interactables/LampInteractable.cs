using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]

public class LampInteractable : Interactable
{
    SphereCollider trigger;
    public bool playerVicino;

    Light light;

    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        //light
        light = GetComponentInChildren<Light>();
        
    }

    void Update()
    {
        playerVicino = IsPlayerNear;
    }

    public override void Interact()
    {
        if (!light.enabled)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
}
