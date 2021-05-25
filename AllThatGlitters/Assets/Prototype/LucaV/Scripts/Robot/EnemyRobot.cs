using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : Robot
{
    private bool isActivated = false;
    private Transform player;
    [SerializeField] private float distanceThreshold = 1f;
    [SerializeField] private float moveSpeed = 2f;

    protected override void Update()
    {
        if (player != null)
        {
            StartCoroutine(FollowPlayer(player));
        }
    }

    protected override void Interact()
    {
        base.Interact();
    }

    protected override void OnPlayerNearby(GameObject _player)
    {
        base.OnPlayerNearby(_player);
        player = _player.transform;
    }

    private IEnumerator FollowPlayer(Transform target)
    {
        while (true)
        {
            if (Vector3.Distance(target.position, agent.destination) > 1f)
            {
                agent.SetDestination(target.position);
            }
            yield return null;
        }
    }
}
