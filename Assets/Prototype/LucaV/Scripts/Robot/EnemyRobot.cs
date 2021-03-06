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
    private Vector3 bezierOffset;
    private float t;

    AudioManager audioM;


    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy;
        agent.speed = moveSpeed;
        lightning = GetComponent<VisualEffect>();
        audioS = GetComponent<AudioSource>();
        lightning.enabled = false;

        audioM = FindObjectOfType<AudioManager>();

    }

    protected override void Update()
    {
        if (player != null && !startedFollowing && !Physics.Raycast(transform.position + transform.up, (player.position + player.up) - (transform.position + transform.up), Vector3.Distance(player.position, transform.position), 1 << LayerMask.NameToLayer("Wall")))
        {
            StartCoroutine(DelayedStart());
            screen.enabled = true;//riattivo la faccia
            //Anim           
            animator.SetTrigger("WakeUp");
            animator.SetBool("isFollowing", true);

            audioM.PlaySound("EnemyDamage");
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
                audioM.StopSound("EnemyDamage");
                //Anim
                animator.SetTrigger("Die");
                screen.enabled = false;//spengo la faccia
                foreach (var c in GetComponents<Collider>())
                {
                    c.isTrigger = true;
                }
                return;
            }

            if (player)
            {
                bool isInLos = !Physics.Raycast(transform.position + transform.up, (player.position + player.up) - (transform.position + transform.up), Vector3.Distance(player.position, transform.position), 1 << LayerMask.NameToLayer("Wall"));


                if (Vector3.Distance(transform.position, player.position) < energyRange && GameManager.singleton.gameState == GameState.playing && isInLos)
                {
                    bezierOffset = transform.position;
                    bezierOffset += transform.forward * 1.5f + transform.up * 2;
                    t += Time.deltaTime * Random.Range(0.1f, 3f);
                    bezierOffset += transform.right * (Mathf.PingPong(t, 1) - .5f);
                    lightning.SetVector3("SecondPointBezier", bezierOffset);
                    lightning.enabled = true;
                    lightning.SetVector3("TargetPos", player.position);
                    float energyStole = energySteal * Time.deltaTime;
                    pStats.ConsumeEnergy(energyStole);
                    currentEnergy += Time.deltaTime;
                }
                else
                {
                    lightning.enabled = false;
                }
            }
        }
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FollowPlayer(player, 0));
        startedFollowing = true;
        isActive = true;
    }

    public override void Interact()
    {
        base.Interact();
    }

    protected override void OnPlayerNearby(GameObject _player)
    {
        base.OnPlayerNearby(_player);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bezierOffset, 1f);
    }

}
