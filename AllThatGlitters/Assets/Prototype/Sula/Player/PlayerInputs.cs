using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    //REF
    PlayerStats stats;
    
    //INPUTS VARI
    public float rawInputHorizontal = 0;
    public float rawInputVertical = 0;
    public bool inputQ;

    //TORCIA 
    [SerializeField]
    Light torch;
    bool isActive;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        ReadInputs();
        Interact();
        //ROBA TORCIA
        LightActivation();
    }

    void ReadInputs()
    {
        rawInputHorizontal = Input.GetAxisRaw("Horizontal");
        rawInputVertical = Input.GetAxisRaw("Vertical");
        inputQ = Input.GetKeyDown(KeyCode.Q);
    }

    void LightActivation()
    {
        if (inputQ)
        {
            if (!isActive)
            {
                torch.enabled = true;
                isActive = true;
                //faccio partire il timer che consuma l'energia
                float timer = 0f;
                timer += Time.deltaTime;

                //play sound
            }
            else
            {
                torch.enabled = false;
                isActive = false;
                //play sound
            }            
        }
    }

    void BatteryUsage()
    {
        //se la torcia è attiva calo di 1 l'energia ogni 2 secondi?? faccio ceh si stacca dasola?? boh vedo

        if (isActive)
        {
            float timer = 0f;
            timer += Time.deltaTime;
            if (timer > 2f)
            {

            }
        }

    }

    void Interact()
    {     
        if (Input.GetKeyDown(KeyCode.E))
        {
            //chiamo le varie funzioni di Luca a seconda delle circostanze       
        }
    }

}
