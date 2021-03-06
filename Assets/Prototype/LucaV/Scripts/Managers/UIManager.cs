using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;

    [Header("Menu Canvas")]
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject menuCanvas;
    [Header("Settings Canvas")]
    [SerializeField] private GameObject settingsCanvas;
    [Header("How To Play Canvas")]
    [SerializeField] private GameObject howToPlayCanvas;
    [Header("Loading Canvas")]
    [SerializeField] private GameObject loadingLevelCanvas;
    [SerializeField] private TMP_Text loadingLevelText;
    [SerializeField] private Slider loadingSlider;
    [Header("Game Canvas")]
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text friendlyBotsText;
    [SerializeField] private TMP_Text missingRobotsText;
    [SerializeField] private TMP_Text fullEnergyText;
    [Header("In Game Menu Canvas")]
    [SerializeField] private GameObject inGameMenuCanvas;
    [Header("Victory Canvas")]
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private TMP_Text durationText;
    [Header("Defeat Canvas")]
    [SerializeField] private GameObject defeatCanvas;
    [Space]
    [SerializeField] private LevelDifficulty[] difficulties;

    private UIState currentState;
    private int currentDiff = 0;
    private string currentDiffName = "";
    private float loadingTimer;

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

    private void OnEnable()
    {
        GameManager.singleton.LevelGenerated += OnLevelGenerated;
    }

    private void OnDisable()
    {
        GameManager.singleton.LevelGenerated -= OnLevelGenerated;
    }

    private void Start()
    {
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        loadingLevelCanvas.SetActive(false);
        inGameMenuCanvas.SetActive(false);
        howToPlayCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        currentState = UIState.mainMenu;

        dropdown.ClearOptions();
        List<string> difficultyNames = new List<string>();
        for (int i = 0; i < difficulties.Length; i++)
        {
            difficultyNames.Add(difficulties[i].difficultyName);
        }
        dropdown.AddOptions(difficultyNames);
        OnDiffChange(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpauseGame();
        }


        if (GameManager.singleton.gameState == GameState.loading)
        {
            loadingTimer += Time.deltaTime * 2.7f;
            int module = (int)loadingTimer % 4;
            string dots = "";
            switch (module)
            {
                case 0:
                    dots = "";
                    break;
                case 1:
                    dots = ".";
                    break;
                case 2:
                    dots = "..";
                    break;
                case 3:
                    dots = "...";
                    break;
                default:
                    break;
            }
            loadingSlider.value = Mathf.Clamp(GameManager.singleton.RoomNumber / (float)GameManager.singleton.MinRooms, 0, 1);
            loadingLevelText.text = "GENERATING LEVEL" + dots;
        }
    }

    public void OnDiffChange(int val)
    {
        currentDiff = val;
        currentDiffName = dropdown.options[currentDiff].text;
    }

    public void OnPlayButton()
    {
        if (currentState != UIState.mainMenu)
            return;
        settingsCanvas.SetActive(false);
        howToPlayCanvas.SetActive(false);
        foreach (var diff in difficulties)
        {
            if (diff.difficultyName == currentDiffName)
            {
                StartLevel(diff);
                break;
            }
        }
    }


    private void StartLevel(LevelDifficulty diff)
    {
        GameManager.singleton.SetDifficulty(diff);
        loadingLevelCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        GameManager.singleton.StartLevelGeneration();
    }

    /// <summary>
    /// Toggle pause menu
    /// </summary>
    /// <param name="isInPause">If is going in pause or out</param>
    public void PauseUnpauseGame()
    {
        if (GameManager.singleton.gameState != GameState.playing && GameManager.singleton.gameState != GameState.pause) { return; }
        
        bool isInPause = GameManager.singleton.gameState == GameState.pause;
        inGameMenuCanvas.SetActive(!isInPause);
        if (!isInPause)
        {
            GameManager.singleton.gameState = GameState.pause;

        }
        else
        {
            GameManager.singleton.gameState = GameState.playing;

        }
    }

    public void UpdateBotsText(int rescuedBots)
    {
        if (friendlyBotsText)
            friendlyBotsText.text = "BOTS RESCUED: " + rescuedBots + "/" + GameManager.singleton.MaxFriendlyRobots;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="percentage">From 0 to 1</param>
    public void UpdateEnergySlider(float percentage)
    {
        if (energySlider)
            energySlider.value = Mathf.Clamp(percentage, 0, 1);
    }

    private void OnLevelGenerated()
    {
        loadingLevelCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void ShowMissingRobots(int val)
    {
        //missingRobotsText.text = "YOU NEED " + val + " MORE ROBOTS!";
        missingRobotsText.text = "NOT ENOUGH ROBOTS";
        missingRobotsText.color = Color.red;
        StartCoroutine(ReduceAlphaOverTime(missingRobotsText));
    }

    public void ShowFullEnergy()
    {
        fullEnergyText.text = "FULL!";
        fullEnergyText.color = Color.red;
        StartCoroutine(ReduceAlphaOverTime(fullEnergyText));
    }

    IEnumerator ReduceAlphaOverTime(TMP_Text txt)
    {
        Color c = txt.color;
        yield return new WaitForSeconds(1f);

        //while (txt.color.a >= 0)
        //{
        //    c.a -= Time.deltaTime;
        //    yield return null;
        //}
        txt.text = "";
        yield return null;
    }

    public void ReturnToMainMenu()
    {
        GameManager.singleton.ResetLevel();
        menuCanvas.SetActive(true);
        inGameMenuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(false);
    }

    public void ShowEndGameCanvas(bool hasPlayerWon)
    {
        inGameMenuCanvas.SetActive(false);
        if (hasPlayerWon)
        {
            victoryCanvas.SetActive(true);
            float timer = GameManager.singleton.playerTimer;
            float minutes = Mathf.Floor(timer / 60);
            float seconds = timer % 60;
            durationText.text = "TIME    " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            defeatCanvas.SetActive(true);
        }
    }

    public void OnSettingsToggle(bool val)
    {
        if (!settingsCanvas.activeSelf && currentState != UIState.mainMenu)
            return;

        currentState = val ? UIState.subMenu : UIState.mainMenu;
        settingsCanvas.SetActive(val);
    }

    public void ToggleHowToPlayCanvas(bool b)
    {
        if (!howToPlayCanvas.activeSelf && currentState != UIState.mainMenu)
            return;

        currentState = b ? UIState.subMenu : UIState.mainMenu;
        howToPlayCanvas.SetActive(b);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}