using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Spawner : MonoBehaviour
{
    public static Action<Spawner> OnUpdateWaveEnnemies;
    public static Action<Spawner> OnUpdateEnemiesToKill;

    [Header ("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private int totalEnemyCountToKill = 10;
    static private int enemyCountToKill = 10;
    [SerializeField] private GameObject testGo;

    [Header ("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    public event OnEnnemiesRemainingChangeDelegate OnEnnemiesRemainingChangeChange;
    public delegate void OnEnnemiesRemainingChangeDelegate(int newVal);

    public LevelManager levelManager;

    private float _spawnTimer;
    private int _ennemiesSpawned;

    private EnemiesPooler _pooler;
    private EventSender _eventSender;

    // Start is called before the first frame update
    void Start()
    {
        _pooler = GetComponent<EnemiesPooler>();
        _eventSender = GameObject.FindObjectOfType<EventSender>();
        _eventSender.OnEnnemiesRemainingChange += VariableChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0 && !LevelManager.EndOfGame && !LevelManager.PauseGame)
        {
            _spawnTimer = delayBtwSpawns;
            if (_ennemiesSpawned < totalEnemyCountToKill)
                SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool(_ennemiesSpawned);
        newInstance.GetComponent<Enemy>().Waypoint = levelManager.CurrentWayPoint;
        newInstance.SetActive(true);
        _ennemiesSpawned++;
        _eventSender.ennemiesRemaining++;
    }

    private void VariableChangeHandler(int newVal)
    {
        OnUpdateEnemiesToKill?.Invoke(this);
        if (newVal < 0)
            newVal = 0;
        if (newVal <= 0 && enemyCountToKill == 0 && !LevelManager.EndOfGame)
        {
            levelManager.WaveCompleted();
        }
    }

    public void SpawnNextWave()
    {
        enemyCount += 5;
        totalEnemyCountToKill += enemyCount;
        levelManager.CurrentWave += 1;
        LevelManager.OnUpdateUILevel?.Invoke(levelManager);
        if (levelManager.CurrentWave > 3)
        {
            _pooler.enemy1Health += 10;
            _pooler.enemy2Health += 10;
            _pooler.enemy3Health += 10;
            _pooler.enemy1MoveSpeed += 0.5f;
            _pooler.enemy2MoveSpeed += 0.3f;
            _pooler.enemy3MoveSpeed += 0.1f;
        }
        _pooler.InitPooler(false);
        _pooler.CreatePooler();
        OnUpdateWaveEnnemies?.Invoke(this);
    }

    public void ResetSpawner()
    {
        _ennemiesSpawned = 0;
        enemyCount += 5;
        totalEnemyCountToKill = enemyCount;
        OnUpdateWaveEnnemies?.Invoke(this);
    }

    public int GetEnnemiesSpawned()
    {
        return _ennemiesSpawned;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public int GetEnemyCountToKill()
    {
        return enemyCountToKill;
    }

    static public void SetEnemyCountToKillAfterKill()
    {
        enemyCountToKill--;
    }

    static public void SetEnemyCountToKill(int newValue)
    {
        enemyCountToKill = newValue;
    }
}
