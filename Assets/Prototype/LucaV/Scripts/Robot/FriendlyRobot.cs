using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRobot : Robot
{
    private bool isActivated = false;
    [SerializeField] private float distanceThreshold = 1f;
    [SerializeField] float EnergyPrice = 5f;
    [SerializeField] GameObject body;
    AudioManager audioM;

    private void Start()
    {
        base.Start();
        audioM = FindObjectOfType<AudioManager>();
    }
    protected override void Update()
    {
        if (player != null && isActivated && !startedFollowing)
        {
            GameManager.singleton.CurrentRescuedRobots++;
            startedFollowing = true;
            StartCoroutine(FollowPlayer(player, distanceThreshold));
        }
    }

    public override void Interact()
    {
        if (!isActivated)
        {
            base.Interact();
            //tolgo energia al player
            pStats.ConsumeEnergy(EnergyPrice);
            ////fermo l'animazione? vabbè per ora metto il movimento normale
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetBool("isFollowing", true);
            ////sparento il corpo?
            //body.transform.parent = null;
            //Debug.Log("body.transform.parent");

            audioM.PlaySound("FollowFriend");
            isActivated = true;
            GameManager.singleton.Player.GetComponent<Interact>().Remove(gameObject.GetComponent<Interactable>());
        }

    }

    protected override void OnPlayerNearby(GameObject _player)
    {
        base.OnPlayerNearby(_player);
    }
}
