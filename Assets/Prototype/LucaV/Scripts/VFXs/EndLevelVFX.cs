using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelVFX : MonoBehaviour
{
    [SerializeField] float moveRange = 1;
    [SerializeField] float moveSpeed = 1;
    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        Vector3 offset = Vector3.zero;
        offset.x = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        offset.z = Mathf.PingPong(Time.time * moveSpeed - moveRange, moveRange * 2) - moveRange;

        transform.position = pos + offset;
    }
}
