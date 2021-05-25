using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [SerializeField] private GameObject startRoom;

    public delegate void RoomsGeneration();
    public event RoomsGeneration RoomsGenerated;
    public Room[] rooms;
    public Room[] endRooms;
    public Room[] hallwayRooms;
    public GameState gameState;
    public Dictionary<Room, float> roomDistances = new Dictionary<Room, float>();
    public GameObject endLevelInteractable;
    public Vector3 startPos { get; set; }

    public int RoomNumber { get; set; }
    public int minRooms;
    public int EnemyRobotsNumber { get; set; }
    public int maxEnemyRobots;
    public int FriendlyRobotsNumber { get; set; }
    public int maxFriendlyRobots;

    private float generateWaitTime = 1f;
    private bool hasGenerated = false;
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
        gameState = GameState.loading;
        Instantiate(startRoom, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (RoomNumber > minRooms)
        {
            generateWaitTime -= Time.deltaTime;
            if (generateWaitTime <= 0 && !hasGenerated)
            {
                hasGenerated = true;
                SetupLevel();
            }
        }
    }

    private void SetupLevel()
    {
        gameState = GameState.playing;
        float maxDistance = 0;
        Room endRoom = null;
        foreach (var room in roomDistances)
        {
            if (room.Value > maxDistance)
            {
                maxDistance = room.Value;
                endRoom = room.Key;
            }
        }
        endRoom.IsEndRoom = true;
        RoomsGenerated?.Invoke();
    }

    public void OnLevelFinish(bool hasPlayerWon)
    {
        gameState = GameState.gameOver;
    }

    public void SendRoomDistance(Room r, float d)
    {
        roomDistances.Add(r, d);
    }
}
