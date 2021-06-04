using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aggiungere il requirement del mesh collider e che sia trigger

public class Battery : Interactable
{   
    public int recharge = 1;

    public override void Interact()
    {
        GameManager.singleton.Player.GetComponent<PlayerStats>().RefillEnergy(1);
    }

}
