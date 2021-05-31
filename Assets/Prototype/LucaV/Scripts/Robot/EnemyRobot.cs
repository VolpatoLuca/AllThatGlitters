using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.AI;

public class EnemyRobot : Robot
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxEnergy;
    [SerializeField] private float energyRange = 5;
    [Tooltip("Energy stole each second")]
    [SerializeField] private float energySteal = 3f;
    private AudioSource audioS;
    private VisualEffect lightning;
    private float currentEnergy;
    private bool isActive;

    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy;
        agent.speed = moveSpeed;
        lightning = GetComponent<VisualEffect>();
        audioS = GetComponent<AudioSource>();
        lightning.enabled = false;
    }

    protected override void Update()
    {
        if (player != null && !startedFollowing)
        {
            startedFollowing = true;
            isActive = true;
            StartCoroutine(FollowPlayer(player, 0));
            audioS.Play();
        }
        if (startedFollowing && isActive)
        {
            currentEnergy -= Time.deltaTime;
            if (currentEnergy <= 0)
            {
                StopAllCoroutines();
                isActive = false;
                agent.isStopped = true;
                lightning.enabled = false;
                audioS.Stop();
                return;
            }

            if (Vector3.Distance(transform.position, player.position) < energyRange && GameManager.singleton.gameState == GameState.playing)
            {
                lightning.enabled = true;
                lightning.SetVector3("TargetPos", player.position);
                pStats.ConsumeEnergy(energySteal * Time.deltaTime);
            }
            else
            {
                lightning.enabled = false;
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
