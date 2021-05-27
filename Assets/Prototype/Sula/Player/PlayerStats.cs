using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //REF
    //UIMANAGER SENNò NON VA LA UI :)

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
        //UI
        //UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));

    }

    public void ConsumeEnergy(int energyConsumed)
    {
        int energyDiff = currentEnergy -= energyConsumed;
        isFull = false;
        //play sound

        if (energyDiff > 0)
        {
            currentEnergy = energyDiff;
            //UI
            //UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));
        }
        else
        {
            currentEnergy = 0;
            Debug.Log("Muerto");
            //UI
            //UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));
        }
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
            //UI
            //non serve aggiornare
        }
        else
        {
            currentEnergy = energySum;
            isFull = false;
            //UI
            //UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));
        }
    }


    float NormalizeEnergy(int maxEnergy, int currentEnergy) //mi serve per dare un valore sensato allo slider DA ZERO A 1
    {        
        float max = (float)maxEnergy;
        float current = (float)currentEnergy;
        float normalizedEnergy = Mathf.InverseLerp(0, max, current);
        return normalizedEnergy;
    }


    void AddSavedRobot()
    {
        robotsSaved++;
        //UI
        //mettere qui la roba del ui manager
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
