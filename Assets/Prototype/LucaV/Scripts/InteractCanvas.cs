using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCanvas : MonoBehaviour
{
    void Update()
    {
        Vector3 rot = Vector3.zero;
        rot.x = 30;
        transform.eulerAngles = rot;
    }
}
