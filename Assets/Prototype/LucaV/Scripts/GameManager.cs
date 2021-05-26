using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [SerializeField] private GameObject startRoom;

    public delegate void ManagerEvent();
    public event ManagerEvent RoomsGenerated;
    public event ManagerEvent LevelReset;
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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameState = GameState.waitingInput;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartLevelGeneration();
        }

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

    private void StartLevelGeneration()
    {
        gameState = GameState.loading;
        Instantiate(startRoom, transform.position, Quaternion.identity);
    }

    public void SetupLevel()
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
    public void SetDifficulty(LevelDifficulty diff)
    {
        maxEnemyRobots = diff.enemyRobotsAmount;
        maxFriendlyRobots = diff.friendlyRobotsAmount;
        minRooms = diff.roomAmount;
    }

    public void OnLevelFinish(bool hasPlayerWon)
    {
        gameState = GameState.gameOver;
    }

    public void SendRoomDistance(Room r, float d)
    {
        roomDistances.Add(r, d);
    }

    public void ResetLevel()
    {
        LevelReset?.Invoke();
        EnemyRobotsNumber = 0;
        RoomNumber = 0;
        FriendlyRobotsNumber = 0;
    }
}
