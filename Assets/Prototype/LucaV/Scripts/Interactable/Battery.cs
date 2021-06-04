using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aggiungere il requirement del mesh collider e che sia trigger

public class Battery : Interactable
{   
    public int recharge = 1;
    AudioManager audioM;

    private void Start()
    {
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
