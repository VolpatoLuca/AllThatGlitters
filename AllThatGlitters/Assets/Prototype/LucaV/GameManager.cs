using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;
    public Room[] rooms;
    public Room[] endRooms;
    public static GameManager singleton;
    public int RoomNumber { get; set; }
    public int minRooms;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Instantiate(startRoom, transform.position, Quaternion.identity);
    }
}
