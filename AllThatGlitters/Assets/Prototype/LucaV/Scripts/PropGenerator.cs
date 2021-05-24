using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] props;

    private void Start()
    {
        int rand = Random.Range(0, props.Length);
        Instantiate(props[rand], transform.position, Quaternion.identity);
    }
}
