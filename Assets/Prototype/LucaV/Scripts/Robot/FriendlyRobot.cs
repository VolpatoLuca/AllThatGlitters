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

    protected override void Start()
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
            //passo l'anim a movement
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetBool("isFollowing", true);
            ////sparento il corpo?
            //body.transform.parent = null;
            //Debug.Log("body.transform.parent");
            //nascondo il corpo e riattivo lo schermo
            body.GetComponent<SkinnedMeshRenderer>().enabled = false;
            screen.GetComponent<SkinnedMeshRenderer>().enabled = true;
            //Rimuovo interactable??
            Destroy(GetComponent<RobotInteract>());

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
