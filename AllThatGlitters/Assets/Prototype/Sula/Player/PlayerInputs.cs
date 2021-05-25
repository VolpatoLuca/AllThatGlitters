using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    
    //INPUTS VARI
    public float rawInputHorizontal = 0;
    public float rawInputVertical = 0;


    //TORCIA 
    [SerializeField]
    Light torch;
    bool isActive;

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
    }

    void LightActivation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isActive)
            {
                torch.enabled = true;
                isActive = true;
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

    void Interact()
    {     
        if (Input.GetKeyDown(KeyCode.E))
        {
            //chiamo le varie funzioni di Luca a seconda delle circostanze       
        }
    }

}
