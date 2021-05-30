using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    PlayerInputs inputs;

    public Interactable availableInteractable;

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable otherInteractable))
        {
            otherInteractable.isPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable otherInteractable))
        {
            otherInteractable.isPlayerNear = false;
        }
    }

}
