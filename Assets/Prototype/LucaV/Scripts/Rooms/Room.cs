using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public CheckPoint[] directions;

    private Camera roomCamera;
    private MeshRenderer mr;
    public bool IsEndRoom { get; set; } = false;
    public bool HasEndInteractable { get; set; } = false;

    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += CheckIsEndRoom;
        GameManager.singleton.LevelReset += DestroyRoom;
    }

    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= CheckIsEndRoom;
        GameManager.singleton.LevelReset -= DestroyRoom;
    }

    private void Start()
    {
        //mr = GetComponent<MeshRenderer>();
        //roomCamera = GetComponentInChildren<Camera>();
        GameManager.singleton.SendRoomDistance(this, Vector3.Distance(transform.position, GameManager.singleton.startPos));

        //if (!roomCamera)
        //    return;
        //roomCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        //mr.material.SetTexture("_RenderTexture", roomCamera.targetTexture);
    }

    private void Update()
    {

        //if (GameManager.singleton.player)
        //    mr.material.SetVector("_Pos", GameManager.singleton.player.transform.position);
    }
    private void CheckIsEndRoom()
    {
    }

    private void DestroyRoom()
    {
        Destroy(gameObject);
    }
}
