using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRobot : Robot
{
    private bool isActivated = false;
    private Transform player;
    [SerializeField] private float distanceThreshold = 1f;

    protected override void Update()
    {
        if (isActivated && Vector3.Distance(transform.position, player.position) > distanceThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 1f * Time.deltaTime);
        }
    }

    protected override void Interact()
    {
        base.Interact();
        isActivated = true;
    }

    protected override void OnPlayerNearby(GameObject _player)
    {
        base.OnPlayerNearby(_player);
        player = _player.transform;
    }
}
