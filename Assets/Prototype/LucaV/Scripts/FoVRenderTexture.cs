using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoVRenderTexture : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private Material floorMat;
    [SerializeField] private Transform mainCam;
    [SerializeField] private Transform player;

    private void Start()
    {
        camera = GetComponent<Camera>();
        //camera.targetTexture = new RenderTexture(Screen.height , Screen.height , 24, RenderTextureFormat.ARGB32, 1);
        //floorMat.SetTexture("_RenderTexture", camera.targetTexture);
        transform.parent = null;
    }

    private void Update()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Vector3.Distance(mainCam.position, player.position);
        transform.position = player.position + pos;
    }
}
