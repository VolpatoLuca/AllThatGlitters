using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    PlayerInputs inputs;
    PlayerStats stats;

    public List<Interactable> availableInteractables;
    public Interactable closestInteractable;

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
        stats = GetComponent<PlayerStats>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable otherInteractable))
        {
            availableInteractables.Add(otherInteractable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable otherInteractable))
        {
            if (closestInteractable == otherInteractable)
                closestInteractable = null;
            availableInteractables.Remove(otherInteractable);
            otherInteractable.IsPlayerNear = false;
        }
    }

    void FindClosestInteractable()
    {
        float maxDistance = Mathf.Infinity;
        closestInteractable = null;

        foreach (var interactable in availableInteractables)
        {
            float targetDistance = Vector3.Distance(transform.position, interactable.transform.position);
            interactable.IsPlayerNear = false;

            if (targetDistance < maxDistance)
            {
                maxDistance = targetDistance;
                closestInteractable = interactable;
            }
        }
        closestInteractable.IsPlayerNear = true;

    }

    private void Update()
    {
        if (availableInteractables.Count > 0)
        {
            FindClosestInteractable();

        }

        if (inputs.inputE && closestInteractable != null)
        {
            if (closestInteractable.TryGetComponent(out Battery battery))
            {
                float energySum = stats.currentEnergy += battery.recharge;

                if (energySum >= stats.maxEnergy)
                {
                    stats.currentEnergy = stats.maxEnergy;
                    Debug.Log("Full Energy");
                    //da dire in ui magari
                    return;
                }
                else
                {
                    closestInteractable.Interact();
                }
            }
            else
            {
            closestInteractable.Interact();
            }
        }

    }

     public void Remove(Interactable otherInteractable)
    {
        availableInteractables.Remove(otherInteractable);
    }
}
