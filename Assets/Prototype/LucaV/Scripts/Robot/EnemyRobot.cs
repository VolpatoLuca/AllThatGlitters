using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : Robot
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxEnergy;
    [SerializeField] private float energyRange = 5;
    [Tooltip("Energy stole each second")]
    [SerializeField] private float energySteal = 3f;
    private float currentEnergy;
    private bool isActive;

    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy;
        agent.speed = moveSpeed;
    }

    protected override void Update()
    {
        if (player != null && !startedFollowing)
        {
            startedFollowing = true;
            isActive = true;
            StartCoroutine(FollowPlayer(player, 0));
        }
        if (startedFollowing && isActive)
        {
            currentEnergy -= Time.deltaTime;
            if (currentEnergy <= 0)
            {
                StopAllCoroutines();
                isActive = false;
                agent.isStopped = true;
            }

            if (Vector3.Distance(transform.position, player.position) < energyRange)
            {
                pStats.ConsumeEnergy(1 * Time.deltaTime);
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
