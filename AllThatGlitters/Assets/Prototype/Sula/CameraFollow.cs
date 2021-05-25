using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 maxCameraOffset;
    Vector3 minCameraOffset;
    Vector3 currentCameraOffset;
    //[SerializeField] float zoomSpeed = 5f; //per ora non serve

    private void Start()
    {
        maxCameraOffset = transform.position;
        minCameraOffset = maxCameraOffset / 2; //boh l'ho messo la metà giusto per ora
        currentCameraOffset = maxCameraOffset;
    }
    private void Update()
    {
        cameraZoom();
        Follow();

    }

    private void Follow()
    {
        Vector3 finalOffset = currentCameraOffset + player.transform.position;
        transform.position = finalOffset;
    }

    void cameraZoom()
    {
        if (Input.mouseScrollDelta != new Vector2(0,0))
        {
            //mi serve solo la y 1, -1
            float yScrollValue = Input.mouseScrollDelta.y;
            //lo uso per lerpare fra max distance e min distance
            currentCameraOffset = Vector3.Lerp(maxCameraOffset, minCameraOffset, yScrollValue);
            //lo vorrei più smooth :((
            Debug.Log(Input.mouseScrollDelta);
        }
    }

}
