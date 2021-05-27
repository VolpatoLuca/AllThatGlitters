using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject inGameMenuCanvas;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text friendlyBotsText;

    [SerializeField] private LevelDifficulty[] difficulties;
    [SerializeField] private LevelDifficulty tutorialDiff;

    private int currentDiff = 0;
    private string currentDiffName = "";

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
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        inGameMenuCanvas.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseUnpauseGame();
        }
    }

    public void OnDiffChange(int val)
    {
        currentDiff = val;
        currentDiffName = dropdown.options[currentDiff].text;
    }

    public void OnPlayButton()
    {
        foreach (var diff in difficulties)
        {
            if (diff.difficultyName == currentDiffName)
            {
                StartLevel(diff);
                break;
            }
        }
    }

    public void OnPlayTutorial()
    {
        StartLevel(tutorialDiff);
    }

    private void StartLevel(LevelDifficulty diff)
    {
        GameManager.singleton.SetDifficulty(diff);
        gameCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        GameManager.singleton.StartLevelGeneration();
    }
    /// <summary>
    /// Toggle pause menu
    /// </summary>
    /// <param name="isInPause">If is going in pause or out</param>
    public void PauseUnpauseGame()
    {
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
            friendlyBotsText.text = "BOTS RESCUED: " + rescuedBots + "/" + GameManager.singleton.maxFriendlyRobots;
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
}