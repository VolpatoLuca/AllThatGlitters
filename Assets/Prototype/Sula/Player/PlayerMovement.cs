using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    PlayerInputs inputs;

    //settings 
    [SerializeField]  [Range(1f,10f)]
    float speed = 5f;
    [SerializeField]
    float turnSmoothingTime = 0.1f;
    [SerializeField]
    float gravity = -0.5f;

    //altro
    float currentVelocity = 0.0f;
    [SerializeField]
    bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        ManageMovement();
        
    }

    private void ManageMovement()
    {

        Vector3 direction = new Vector3(inputs.rawInputHorizontal, isGrounded ? 0.0f : gravity, inputs.rawInputVertical).normalized; //Vettore di movimento
        
        //ISGROUNDED � RETARD   

        if (direction.magnitude >= 0.1f)
        {
            RotateTowardsMovement(direction);
            controller.Move(direction * speed *Time.deltaTime);
            
        }
    }

    private void RotateTowardsMovement(Vector3 direction)
    {
        //HO BISOGNO DELL'ANGOLAZIONE DEL VETTORE DI MOVIMENTO PER RUOTARE IL PLAYER
        //ATAN2 MI RESTITUISCE L'ANGOLO IN RADIANS
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //VOGLIO SMOOTHARE LA ROTAZIONE
        float rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, turnSmoothingTime);
        //RUOTO :)
        transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }

    


}