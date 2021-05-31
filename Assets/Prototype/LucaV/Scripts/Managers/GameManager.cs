using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [Header("ROOMS")]
    [SerializeField] private GameObject startRoom;
    public Room[] rooms;
    public Room[] endRooms;
    public Room[] hallwayRooms;

    public delegate void ManagerEvent();
    public event ManagerEvent RoomsGenerated;
    public event ManagerEvent LevelGenerated;
    public event ManagerEvent LevelReset;
    [Header("Assets")]
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Material floorMat;
    public GameObject playerPrefab;
    public GameObject Player { get; set; }
    public GameObject endLevelInteractable;
    public GameState gameState;
    [HideInInspector] public Dictionary<Room, float> roomDistances = new Dictionary<Room, float>();
    [HideInInspector] public List<PropGenerator> enemyGenerators = new List<PropGenerator>();
    [HideInInspector] public List<PropGenerator> friendsGenerators = new List<PropGenerator>();
    public Vector3 startPos { get; set; }
    public int CurrentRescuedRobots { get; set; }
    public int RoomNumber { get; set; }
    public float playerTimer { get; set; }
    public int MinRooms { get; set; }
    [HideInInspector] public int enemyRobotsNumber;
    public int MaxEnemyRobots { get; set; }
    [HideInInspector] public int friendlyRobotsNumber;
    public int MaxFriendlyRobots { get; set; }

    private float generateWaitTime = 1f;
    private bool hasGeneratedRooms = false;
    private bool hasGeneratedLevel = false;
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

        if (enemyRobotsNumber >= MaxEnemyRobots && friendlyRobotsNumber >= MaxFriendlyRobots && !hasGeneratedLevel && gameState == GameState.loading)
        {
            hasGeneratedLevel = true;
            gameState = GameState.playing;
            LevelGenerated?.Invoke();
        }

        if (RoomNumber > MinRooms)
        {
            generateWaitTime -= Time.deltaTime;
            if (generateWaitTime <= 0 && !hasGeneratedRooms)
            {
                hasGeneratedRooms = true;
                SetupLevel();
            }
        }

        if (gameState == GameState.playing)
        {
            playerTimer += Time.deltaTime;
            UIManager.singleton.UpdateBotsText(CurrentRescuedRobots);
            floorMat.SetVector("_Pos", Player.transform.position);
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
        gameState = GameState.loading;
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
        Player = Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
        endRoom.IsEndRoom = true;
        if (enemyGenerators.Count > 0)
            SelectSpawners(enemyGenerators, MaxEnemyRobots, out enemyRobotsNumber);
        if (friendsGenerators.Count > 0)
            SelectSpawners(friendsGenerators, MaxFriendlyRobots, out friendlyRobotsNumber);
        Camera.main.gameObject.SetActive(false);
        RoomsGenerated?.Invoke();
    }

    private void SelectSpawners(List<PropGenerator> list, int spawnAmount, out int spawnCount)
    {
        spawnCount = 0;
        for (int i = 0; i < spawnAmount; i++)
        {
            spawnCount++;
            int rand = Random.Range(0, list.Count);
            list[rand].canSpawn = true;
            list.RemoveAt(rand);
        }
    }

    public void SetDifficulty(LevelDifficulty diff)
    {
        MaxEnemyRobots = diff.enemyRobotsAmount;
        MaxFriendlyRobots = diff.friendlyRobotsAmount;
        MinRooms = diff.roomAmount;
    }

    public void OnLevelFinish(bool hasPlayerWon)
    {
        gameState = GameState.gameOver;
        UIManager.singleton.ShowEndGameCanvas(hasPlayerWon);
    }

    public void SendRoomDistance(Room r, float d)
    {
        roomDistances.Add(r, d);
    }

    public void ResetLevel()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        gameState = GameState.waitingInput;
        LevelReset?.Invoke();
        enemyRobotsNumber = 0;
        RoomNumber = 0;
        friendlyRobotsNumber = 0;
        enemyGenerators.Clear();
        friendsGenerators.Clear();
        roomDistances.Clear();
        generateWaitTime = 1;
        hasGeneratedRooms = false;
        CurrentRescuedRobots = 0;
        hasGeneratedLevel = false;
        playerTimer = 0;
    }

    public void EndLevelInteracted()
    {
        if (CurrentRescuedRobots >= MaxFriendlyRobots)
        {
            OnLevelFinish(true);
        }
        else
        {
            UIManager.singleton.ShowMissingRobots(MaxFriendlyRobots - friendlyRobotsNumber);
        }
    }
}
