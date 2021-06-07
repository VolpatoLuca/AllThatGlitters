using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(NavMeshAgent))]
public abstract class Robot : MonoBehaviour
{

    protected bool startedFollowing = false;
    protected Transform player;
    protected PlayerStats pStats;
    protected NavMeshAgent agent;

    [SerializeField] protected GameObject screen;
    protected Animator animator;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        screen.GetComponent<SkinnedMeshRenderer>().enabled = false; //disattivo la faccia
        animator = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {

    }

    public virtual void Interact()
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
        if (!pStats)
            pStats = player.GetComponent<PlayerStats>();
    }

    protected IEnumerator FollowPlayer(Transform target, float offset)
    {
        while (true)
        {
            if (GameManager.singleton.gameState != GameState.playing)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;

                if (Vector3.Distance(target.position - (transform.position - target.position).normalized * offset, agent.destination) > 1f)
                {
                    agent.SetDestination(target.position);
                }
            }
            yield return null;
        }
    }
}
