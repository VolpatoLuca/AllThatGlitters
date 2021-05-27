using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [SerializeField] private GameObject startRoom;
    [SerializeField] private Texture2D cursorTexture;

    public delegate void ManagerEvent();
    public event ManagerEvent RoomsGenerated;
    public event ManagerEvent LevelReset;
    public Room[] rooms;
    public Room[] endRooms;
    public Room[] hallwayRooms;
    [HideInInspector] public GameState gameState;
    [HideInInspector] public Dictionary<Room, float> roomDistances = new Dictionary<Room, float>();
    [HideInInspector] public GameObject endLevelInteractable;
    [HideInInspector] public List<PropGenerator> enemyGenerators = new List<PropGenerator>();
    [HideInInspector] public List<PropGenerator> friendsGenerators = new List<PropGenerator>();
    public Vector3 startPos { get; set; }
    public int RoomNumber { get; set; }
    public int minRooms;
    public int EnemyRobotsNumber { get; set; }
    public int maxEnemyRobots;
    public int FriendlyRobotsNumber { get; set; }
    public int maxFriendlyRobots;

    private float generateWaitTime = 1f;
    private bool hasGenerated = false;
    private CursorMode cursorMode = CursorMode.Auto;
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
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartLevelGeneration();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
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

    /// <summary>
    /// Generates level
    /// </summary>
    public void StartLevelGeneration()
    {
        gameState = GameState.loading;
        Instantiate(startRoom, transform.position, Quaternion.identity);
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
        if (enemyGenerators.Count > 0)
            SelectSpawners(enemyGenerators, maxEnemyRobots);
        if (friendsGenerators.Count > 0)
            SelectSpawners(friendsGenerators, maxFriendlyRobots);

        RoomsGenerated?.Invoke();
    }

    private void SelectSpawners(List<PropGenerator> list, int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int rand = Random.Range(0, list.Count);
            list[rand].canSpawn = true;
            list.RemoveAt(rand);
        }
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
        enemyGenerators.Clear();
        friendsGenerators.Clear();
        roomDistances.Clear();
        generateWaitTime = 1;
        hasGenerated = false;
    }
}
