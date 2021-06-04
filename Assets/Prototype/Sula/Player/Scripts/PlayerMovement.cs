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
    [SerializeField]
    Animator animator;

    //settings 
    [SerializeField]
    [Range(1f, 10f)]
    float speed = 5f;
    [SerializeField]
    float turnSmoothingTime = 0.1f;
    [SerializeField]
    float gravity = -0.5f;

    //altro
    float currentVelocity = 0.0f;
    [SerializeField]
    bool isGrounded;

    //rotation stuff
    public Vector3 worldPosOnPlane;
    Vector3 target;
    Vector3 walkDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputs = GetComponent<PlayerInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        //if (GameManager.singleton.gameState == GameState.playing)
        //{
        LookAtCursor();
        ManageMovement();
        //}
    }

    private void ManageMovement()
    {
        
        Vector3 direction = new Vector3(inputs.rawInputHorizontal, isGrounded ? -0.1f : gravity, inputs.rawInputVertical).normalized; //Vettore di movimento
        walkDirection = new Vector3(inputs.rawInputHorizontal, 0, inputs.rawInputVertical).normalized; //senza gravità per vedere se cammina

        //if (direction.magnitude >= 0.1f)
        //{
            controller.Move(direction * speed * Time.deltaTime);
            //ANIMATION Walk
            if (walkDirection.magnitude >= 0.1f)
            {
                animator.SetBool("isWalking", true);
                AnimationRotation();                
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
            
        //}       
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

    void LookAtCursor()
    {
        Plane plane = new Plane(Vector3.up, 0); //creo un piano
        if (plane.Raycast(inputs.mouseRay, out float distance))
        {
            worldPosOnPlane = inputs.mouseRay.GetPoint(distance);
            target = new Vector3(worldPosOnPlane.x, transform.position.y,worldPosOnPlane.z );
            transform.LookAt(target);
        }

    }

    void AnimationRotation()
    {
        Vector3 playerFWd = transform.forward.normalized;
        Vector3 sum = (walkDirection + playerFWd);
        
        //Debug.Log("magnitude"  + sum.magnitude);
        // magnitude va da 0 a 2

        if (sum.magnitude >= 1f)
        {
            if (!animator.GetBool("isWalkingFwd"))
            {
                //Debug.Log("avanti");
                animator.SetBool("isWalkingFwd", true);
                animator.SetBool("isWalkingBack", false);
            }
        }
        else if (sum.magnitude < 1f)
        {
            if (!animator.GetBool("isWalkingBack"))
            {
                //Debug.Log("indietro");
                animator.SetBool("isWalkingBack", true);
                animator.SetBool("isWalkingFwd", false);
            }
        }
   
    }


}
