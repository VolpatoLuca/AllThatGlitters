using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    //REF
    PlayerStats stats;
    PlayerInputs inputs;

    [SerializeField]
    Light torch;
    bool isActive;
    [SerializeField]
    int lightActivationPrice = 1;
    [SerializeField]
    int lightUsagePrice = 1;
    [SerializeField]
    int lightUsageDelay = 1;
    [SerializeField]
    int maxLoopDiConsumo = 5; //da rinominare
    [SerializeField]
    int torchDuration; // so che sarà n° di loop per secondi di attesa maxLoopDiConsumo*lightUsageDelay. vorrei far editare questo e non maxLoopDiConsumo
    [SerializeField]
    bool enoughEnergy;



    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        inputs = GetComponent<PlayerInputs>();
    }


    private void Update()
    {     
        LightActivation();
    }


    void LightActivation()
    {
        if (inputs.inputQ)
        {
            EnergySafety();

            if (!enoughEnergy)
            {
                Debug.Log("Not enough energy");
                return;
            }
            if (!isActive && enoughEnergy)
            {
                TurnOn();
            }
            else if(isActive) //se è accesa
            {
                TurnOff();
            }
        }
    }

    private void TurnOn()
    {
        torch.enabled = true;
        isActive = true;
        StartCoroutine("BatteryUsage"); //faccio partire il timer che consuma l'energia
        //play sound
    }

    private void TurnOff()
    {
        torch.enabled = false;
        isActive = false;
        //play sound
    }

    IEnumerator BatteryUsage()
    {            
        stats.ConsumeEnergy(lightActivationPrice); //consumo all'attivazione

        for (int i = 0; i < maxLoopDiConsumo; i++)
        {
            stats.ConsumeEnergy(lightUsagePrice); // consumo nel tempo
            yield return new WaitForSeconds(lightUsageDelay); //ogni quanto consumo
        }

        TurnOff();
    }

    void EnergySafety()
    {
        int energyRequirement = lightActivationPrice + (lightUsagePrice * maxLoopDiConsumo) + 1; //il +1 è per non far morire il giocatore

        if (stats.currentEnergy > energyRequirement)//voglio che abbia abbastanza energia per una accensione lunga (loop della torcia)
        {
            enoughEnergy = true;
        }
        else
        {
            enoughEnergy = false;
        }
    }


   

}
