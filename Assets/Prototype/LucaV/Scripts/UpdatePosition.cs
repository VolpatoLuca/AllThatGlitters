using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    private MeshRenderer mr;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        mr.sharedMaterial.SetVector("_Pos", new Vector4(transform.position.x, 0, transform.position.z, 0));
    }
}
