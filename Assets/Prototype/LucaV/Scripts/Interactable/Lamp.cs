using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Lamp : Interactable
{
    [SerializeField] private new Light light;
    private float lifeTime;
    private float currentLifeTime;
    public override void Interact()
    {
        light.enabled = !light.enabled;
        currentLifeTime = lifeTime;
    }

    protected override void Update()
    {
        base.Update();
        currentLifeTime = Mathf.Clamp(currentLifeTime -= Time.deltaTime, 0, lifeTime);
        if(currentLifeTime <= 0 && light.enabled)
        {
            light.enabled = false;
        }
    }
}
