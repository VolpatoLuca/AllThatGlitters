using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{    
    //INPUTS VARI
    public float rawInputHorizontal = 0;
    public float rawInputVertical = 0;
    public bool inputQ;


    private void Update()
    {
        ReadInputs();
        Interact();       
    }

    void ReadInputs()
    {
        rawInputHorizontal = Input.GetAxisRaw("Horizontal");
        rawInputVertical = Input.GetAxisRaw("Vertical");
        inputQ = Input.GetKeyDown(KeyCode.Q);
    }

    void Interact()
    {     
        if (Input.GetKeyDown(KeyCode.E))
        {
            //chiamo le varie funzioni di Luca a seconda delle circostanze       
        }
    }

}
