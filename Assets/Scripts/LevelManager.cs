using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;

    public static LevelManager Instance { get; private set; }

    public static Action<LevelManager> OnUpdateUILevel;

    public Waypoint CurrentWayPoint;
    public Spawner spawner;
    public EnemiesPooler enemiesPooler;

    public int TotalLives;
    public int CurrentWave;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        Instance = this;
        TotalLives = lives;
        CurrentWave = 1;
        enemiesPooler.CreatePooler();
    }

    private void Start() {
        OnUpdateUILevel?.Invoke(this);
        Spawner.OnUpdateWaveEnnemies?.Invoke(spawner);
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

    private void GameOver()
    {
        Debug.Log("GAMEOVER");
    }

    public void WaveCompleted()
    {
        if (TotalLives > 0)
        {
            Debug.Log("WAVECOMPLETED");
            CurrentWave += 1;
            OnUpdateUILevel?.Invoke(this);
            if (CurrentWave > 3)
            {
                enemiesPooler.enemy1Health += 10;
                enemiesPooler.enemy2Health += 10;
                enemiesPooler.enemy3Health += 10;
                enemiesPooler.enemy1MoveSpeed += 0.5f;
                enemiesPooler.enemy2MoveSpeed += 0.3f;
                enemiesPooler.enemy3MoveSpeed += 0.1f;
            }
            enemiesPooler.InitPooler();
            enemiesPooler.CreatePooler();
            spawner.ResetSpawner();
        }
        else
            GameOver();
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
    }
}
