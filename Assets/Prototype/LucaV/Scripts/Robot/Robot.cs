using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(NavMeshAgent))]
public abstract class Robot : MonoBehaviour
{

    protected bool isFollowing = false;
    protected Transform player;
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

    protected void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out PlayerController))
        //{
        if (other.CompareTag("Player"))
            OnPlayerNearby(other.gameObject);
        //}
    }

    protected virtual void OnPlayerNearby(GameObject _player)
    {
        player = _player.transform;
    }

    protected IEnumerator FollowPlayer(Transform target, float offset)
    {
        while (true)
        {
            if (Vector3.Distance(target.position - (transform.position - target.position).normalized * offset, agent.destination) > 1f)
            {
                agent.SetDestination(target.position);
            }
            yield return null;
        }
    }
}
