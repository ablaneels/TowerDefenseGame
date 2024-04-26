using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;

    public static LevelManager Instance { get; private set; }

    public static Action<LevelManager> OnUpdateUILevel;
    public static bool EndOfGame { get; private set; }
    public static bool PauseGame { get; private set; }

    public Waypoint CurrentWayPoint;
    public Spawner spawner;
    public UIManager uIManager;
    public EnemiesPooler enemiesPooler;

    public int TotalLives;
    public int TotalWaves;
    public int CurrentWave;
    public int newScore;
    public List<int> scores;

    public string currentMap;
    public string currentMode;

    public List<GameObject> maps;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        Instance = this;
        EndOfGame = false;
        PauseGame = false;
        if (PlayerPrefs.HasKey("Map") && PlayerPrefs.HasKey("Mode"))
        {
            currentMap = PlayerPrefs.GetString("Map");
            currentMode = PlayerPrefs.GetString("Mode");
            SetUpMode();
            SetUpMap();
        }
        else
        {
            GoToMenuScene();
        }

        if (PlayerPrefs.HasKey(currentMap + "_" + currentMode + "_Score"))
        {
            string[] splitString = PlayerPrefs.GetString(currentMap + "_" + currentMode + "_Score").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            scores = new List<int>();
            foreach (string item in splitString)
            {
                try
                {
                    scores.Add(Convert.ToInt32(item));
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Value in string was not an int.");
                    Debug.LogException(e);
                }
            }
        }
        else
        {
            scores = new List<int>();
        }

        //TotalLives = lives;
        CurrentWave = 1;
        enemiesPooler.CreatePooler();
    }

    private void Start() {
        OnUpdateUILevel?.Invoke(this);
        Spawner.OnUpdateWaveEnnemies?.Invoke(spawner);
    }

    void Update()
    {
        if (Input.GetKey("escape") || Input.GetKey("space"))
        {
            PauseGame = true;
            uIManager.ShowPauseUI();
        }
    }

    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void UpdateScore(int points)
    {
        newScore += points;
    }

    private void UpdateScore(Enemy enemy)
    {
        UpdateScore(enemy.DeathCoinReward);
    }

    private void GameOver()
    {
        EndOfGame = true;
        enemiesPooler.EndOfGame();
        scores.Add(newScore);
        uIManager.lostUI.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = newScore.ToString();
        Debug.Log("GAMEOVER");
        uIManager.ShowYouLostUI();
    }

    private void WinGame()
    {
        EndOfGame = true;
        scores.Add(newScore);
        uIManager.winUI.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = newScore.ToString();
        Debug.Log("WINGAME");
        uIManager.ShowYouWinUI();
    }

    public void WaveCompleted()
    {
        if (TotalLives > 0)
        {
            Debug.Log("WAVECOMPLETED");
            CurrentWave += 1;
            OnUpdateUILevel?.Invoke(this);
            if (CurrentWave > TotalWaves && TotalWaves != 0)
            {
                WinGame();
            }
            else
            {
                if (CurrentWave > 3)
                {
                    if (currentMode == "Mode3")
                    {
                        enemiesPooler.enemy1Health += 20;
                        enemiesPooler.enemy2Health += 20;
                        enemiesPooler.enemy3Health += 20;
                    }
                    enemiesPooler.enemy1MoveSpeed += 0.5f;
                    enemiesPooler.enemy2MoveSpeed += 0.3f;
                    enemiesPooler.enemy3MoveSpeed += 0.1f;
                }
                enemiesPooler.InitPooler(true);
                enemiesPooler.CreatePooler();
                spawner.ResetSpawner();
            }
        }
        else
            GameOver();
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        EnemyHealth.OnEnemyKilled += UpdateScore;

    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        EnemyHealth.OnEnemyKilled -= UpdateScore;
    }

    public void TryAgain()
    {
        string str = "";
        for (int i = 0; i < scores.Count; i++)
        {
            str += scores[i];
            if (i < scores.Count - 1)
            str += " ";
        }
        PlayerPrefs.SetString(currentMap + "_" + currentMode + "_Score", str);
        PlayerPrefs.Save();
        StartCoroutine(LoadAsyncchronously("GameScene"));
    }
    
    public void GoBackToMenu()
    {
        string str = "";
        for (int i = 0; i < scores.Count; i++)
        {
            str += scores[i];
            if (i < scores.Count - 1)
            str += " ";
        }
        PlayerPrefs.SetString(currentMap + "_" + currentMode + "_Score", str);
        PlayerPrefs.Save();
        GoToMenuScene();
    }

    public void PlayGame()
    {
        PauseGame = false;
        uIManager.HidePauseUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetUpMode()
    {
        switch(currentMode)
        {
            case "Mode1":
                TotalLives = 10;
                TotalWaves = 10;
                break;
            case "Mode2":
                TotalLives = 1;
                TotalWaves = 0;
                break;
            case "Mode3":
                TotalLives = 10;
                TotalWaves = 0;
                break;
            default:
                break;
        }
    }

    private void SetUpMap()
    {
        switch(currentMap)
        {
            case "Map1":
                maps[0].SetActive(true);
                CurrentWayPoint = maps[0].GetComponent<Waypoint>();
                break;
            case "Map2":
                maps[1].SetActive(true);
                CurrentWayPoint = maps[1].GetComponent<Waypoint>();
                break;
            case "Map3":
                maps[2].SetActive(true);
                CurrentWayPoint = maps[2].GetComponent<Waypoint>();
                break;
            case "Map4":
                maps[3].SetActive(true);
                CurrentWayPoint = maps[3].GetComponent<Waypoint>();
                break;
            default:
                break;
        }
    }

    public void GoToMenuScene()
    {
        StartCoroutine(LoadAsyncchronously("MenuScene"));
    }

    IEnumerator LoadAsyncchronously(string sceneStr)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneStr);

        while (operation.isDone == false)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
