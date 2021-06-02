using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    private new Light light;
    [SerializeField] private float lifeTime = 10;
    private float currentLifeTime;

    public override void Interact()
    {
        light.enabled = !light.enabled;
        currentLifeTime = lifeTime;
    }
    protected override void Start()
    {
        base.Start();
        light = GetComponentInChildren<Light>();
    }
    protected override void Update()
    {
        base.Update();
        currentLifeTime = Mathf.Clamp(currentLifeTime -= Time.deltaTime, 0, lifeTime);
        if (currentLifeTime <= 0 && light.enabled)
        {
            light.enabled = false;
        }
    }
}
