using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{    
    //INPUTS VARI
    public float rawInputHorizontal = 0;
    public float rawInputVertical = 0;
    public bool inputQ, inputE;
    public Vector3 mousePos;     
    public Ray mouseRay;
    
    private void Update()
    {
        ReadInputs();      
    }

    void ReadInputs()
    {
        rawInputHorizontal = Input.GetAxisRaw("Horizontal");
        rawInputVertical = Input.GetAxisRaw("Vertical");
        inputQ = Input.GetKeyDown(KeyCode.Q);
        inputE = Input.GetKeyDown(KeyCode.E);
        mousePos = Input.mousePosition;
        mouseRay = Camera.main.ScreenPointToRay(mousePos);//ray from mouse
    }

}
