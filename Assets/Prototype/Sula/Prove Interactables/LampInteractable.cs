using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]

public class LampInteractable : Interactable
{
    SphereCollider trigger;


    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        
    }

}
