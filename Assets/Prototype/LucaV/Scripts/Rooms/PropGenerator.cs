using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] props;
    public bool canSpawn = false;
    private float waitTime = 0.1f;
    private GameObject spawn;

    private void Start()
    {
        int rand = Random.Range(0, props.Length);
        if (props[rand].TryGetComponent(out Robot _))
        {
            spawn = props[rand];
            if (spawn.TryGetComponent(out EnemyRobot _))
            {
                GameManager.singleton.enemyGenerators.Add(this);
            }
            else
            {
                print("non dovrei printare");
                GameManager.singleton.friendsGenerators.Add(this);
            }
        }
        else
        {
            Instantiate(props[rand], transform.position, transform.rotation).transform.parent = transform;
        }
    }

    private void Update()
    {
        if (canSpawn)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                Instantiate(spawn, transform.position, Quaternion.identity).transform.parent = transform;
                canSpawn = false;
            }
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Vector3.up, new Vector3(1, 2, 1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.up, new Vector3(2, 2, 2));
    }
#endif

}
