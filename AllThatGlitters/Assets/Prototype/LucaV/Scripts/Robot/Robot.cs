using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(NavMeshAgent))]
public abstract class Robot : MonoBehaviour
{

    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    protected virtual void Update()
    {

    }

    protected virtual void Interact()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out PlayerController))
        //{
        if (other.CompareTag("Player"))
            OnPlayerNearby(other.gameObject);
        //}
    }

    protected virtual void OnPlayerNearby(GameObject player)
    {

    }
}
