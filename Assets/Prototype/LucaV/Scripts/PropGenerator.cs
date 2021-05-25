using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] props;
    private bool canSpawn = false;
    private float waitTime = 0.1f;
    private GameObject spawn;

    private void Start()
    {
        int rand = Random.Range(0, props.Length);
        if (props[rand].TryGetComponent(out Robot _))
        {
            GameManager.singleton.RoomsGenerated += NavMeshReady;
            spawn = props[rand];
        }
        else
            Instantiate(props[rand], transform.position, Quaternion.identity).transform.parent = transform;
    }

    private void Update()
    {
        if (canSpawn)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                if (spawn.TryGetComponent(out EnemyRobot _))
                {
                    if (++GameManager.singleton.EnemyRobotsNumber <= GameManager.singleton.maxEnemyRobots)
                        Instantiate(spawn, transform.position, Quaternion.identity).transform.parent = transform;
                }
                else
                {
                    if (++GameManager.singleton.FriendlyRobotsNumber <= GameManager.singleton.maxFriendlyRobots)
                        Instantiate(spawn, transform.position, Quaternion.identity).transform.parent = transform;
                }
                canSpawn = false;
            }
        }
    }

    private void NavMeshReady()
    {
        canSpawn = true;
    }

}
