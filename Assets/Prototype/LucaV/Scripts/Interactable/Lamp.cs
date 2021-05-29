using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Lamp : Interactable
{
    [SerializeField] private new Light light;
    public override void Interact()
    {
        light.enabled = !light.enabled;
    }
}
