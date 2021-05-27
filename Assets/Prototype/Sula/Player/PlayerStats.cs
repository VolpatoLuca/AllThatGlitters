using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Energy
    //[SerializeField]
    public int maxEnergy = 10;
    //[SerializeField]
    public int currentEnergy;


    public bool isFull { get; private set; }


    //FRIENDLY ROBOTS
    public int robotsSaved = 0;

    private void Start()
    {
        currentEnergy = maxEnergy;
        isFull = true;
    }

    public void ConsumeEnergy(int energyConsumed)
    {

        int energyDiff = currentEnergy -= energyConsumed;
        isFull = false;
        //play sound

        if (energyDiff > 0)
        {
            currentEnergy = energyDiff;            
        }
        else
        {
            currentEnergy = 0;
            Debug.Log("Muerto");
        }

        //currentEnergy = (energyDiff > 0) ? energyDiff : 0; JUST FOR FUN

    }

    public void RefillEnergy(int energyRefill)
    {
        int energySum = currentEnergy += energyRefill;
        //play sound
        if (energySum>=maxEnergy)
        {
            currentEnergy = maxEnergy;
            Debug.Log("Full Energy");
            isFull = true;
        }
        else
        {
            currentEnergy = energySum;
            isFull = false;
        }

    }



    private void Update()
    {



        //ROBINE DI PROVA
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ConsumeEnergy(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RefillEnergy(1);
        }
    }
}
