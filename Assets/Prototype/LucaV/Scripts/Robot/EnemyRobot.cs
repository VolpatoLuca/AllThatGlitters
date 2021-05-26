using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : Robot
{
    private bool isActivated = false;
    [SerializeField] private float distanceThreshold = 1f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxEnergy;
    private float currentEnergy;
    private bool isActive;

    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy;
    }

    protected override void Update()
    {
        if (player != null && !isFollowing)
        {
            isFollowing = true;
            isActive = true;
            StartCoroutine(FollowPlayer(player, 0));
        }
        if (isFollowing && isActive)
        {
            currentEnergy -= Time.deltaTime;
            if (currentEnergy <= 0)
            {
                StopAllCoroutines();
                isActive = false;
                agent.isStopped = true;
            }
            
            if(Vector3.Distance(transform.position, player.position) < 2f)
            {
                //remove player's energy
            }
        }
    }

    protected override void Interact()
    {
        base.Interact();
    }

    protected override void OnPlayerNearby(GameObject _player)
    {
        base.OnPlayerNearby(_player);
    }

}
