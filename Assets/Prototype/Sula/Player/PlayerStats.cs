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
    public float currentEnergy;


    public bool isFull { get; private set; }


    //FRIENDLY ROBOTS
    public int robotsSaved = 0;

    private void Start()
    {
        currentEnergy = maxEnergy;
        isFull = true;
        //UI
        //UIManager.singleton.UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));

    }

    public void ConsumeEnergy(float energyConsumed)
    {
        float energyDiff = currentEnergy -= energyConsumed;
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
        //UI
        UIManager.singleton.UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));
    }

    public void RefillEnergy(int energyRefill)
    {
        float energySum = currentEnergy += energyRefill;
        //play sound
        if (energySum >= maxEnergy)
        {
            currentEnergy = maxEnergy;
            Debug.Log("Full Energy");
            isFull = true;
        }
        else
        {
            currentEnergy = energySum;
            isFull = false;
            //UI
            UIManager.singleton.UpdateEnergySlider(NormalizeEnergy(maxEnergy, currentEnergy));
        }
    }


    float NormalizeEnergy(int maxEnergy, float currentEnergy) //mi serve per dare un valore sensato allo slider DA ZERO A 1
    {
        float max = maxEnergy;
        float current = (float)currentEnergy;
        float normalizedEnergy = Mathf.InverseLerp(0, max, current);
        return normalizedEnergy;
    }


    void AddSavedRobot()
    {
        robotsSaved++;
        //UI
        GameManager.singleton.CurrentRescuedRobots = robotsSaved;
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
