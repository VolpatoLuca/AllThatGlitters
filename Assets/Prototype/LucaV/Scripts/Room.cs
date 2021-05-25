using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public CheckPoint[] directions;
    public bool IsEndRoom { get; set; } = false;
    public bool HasEndInteractable { get; set; } = false;

    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += CheckIsEndRoom;
    }

    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= CheckIsEndRoom;
    }

    private void Start()
    {
        GameManager.singleton.SendRoomDistance(this, Vector3.Distance(transform.position, GameManager.singleton.startPos));
    }

    private void CheckIsEndRoom()
    {
        if (IsEndRoom)
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = Color.red;
            }
        }
    }
}
