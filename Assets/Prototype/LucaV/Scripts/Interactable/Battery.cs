using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Battery : Interactable
{   
    public int recharge = 1;
    AudioManager audioM;

    protected override void Start()
    {
        base.Start();
        audioM = FindObjectOfType<AudioManager>();
    }

    public override void Interact()
    {
        GameManager.singleton.Player.GetComponent<PlayerStats>().RefillEnergy(recharge);
        GameManager.singleton.Player.GetComponent<Interact>().Remove(gameObject.GetComponent<Interactable>());
        audioM.PlaySound("Recharge");
        Destroy(gameObject);

        
    }

}
