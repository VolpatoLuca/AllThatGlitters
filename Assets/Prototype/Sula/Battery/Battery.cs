using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aggiungere il requirement del mesh collider e che sia trigger

public class Battery : MonoBehaviour
{   
    public int recharge = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
            if (!stats.isFull)
            {
                stats.RefillEnergy(recharge);
                Debug.Log("raccolgo");
                Destroy(gameObject);
            }
        }
    }
}
