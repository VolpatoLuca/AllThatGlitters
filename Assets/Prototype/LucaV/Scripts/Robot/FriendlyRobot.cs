using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRobot : Robot
{
    private bool isActivated = false;
    [SerializeField] private float distanceThreshold = 1f;

    protected override void Update()
    {
        if (player != null && isActivated && !isFollowing)
        {
            isFollowing = true;
            StartCoroutine(FollowPlayer(player, .5f));
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
    }


}
