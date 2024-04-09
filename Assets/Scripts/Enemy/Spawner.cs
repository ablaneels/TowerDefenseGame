using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Spawner : MonoBehaviour
{
    public static Action<Spawner> OnUpdateWaveEnnemies;

    [Header ("Settings")]
    [SerializeField] private int ennemyCount = 10;
    [SerializeField] private int ennemyCountToKill = 10;
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
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_ennemiesSpawned < ennemyCountToKill)
                SpawnEnnemy();
        }
    }

    private void SpawnEnnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool(_ennemiesSpawned);
        newInstance.GetComponent<Enemy>().Waypoint = levelManager.CurrentWayPoint;
        newInstance.SetActive(true);
        _ennemiesSpawned++;
        _eventSender.ennemiesRemaining++;
    }

    private void VariableChangeHandler(int newVal)
    {
        if (newVal < 0)
            newVal = 0;
        if (newVal <= 0 && _ennemiesSpawned == ennemyCountToKill && !LevelManager.EndOfGamel)
        {
            levelManager.WaveCompleted();
        }
    }

    public void SpawnNextWave()
    {
        ennemyCount += 5;
        ennemyCountToKill += (ennemyCount + 5);
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
        _pooler.CreatePooler();
        OnUpdateWaveEnnemies?.Invoke(this);
    }

    public void ResetSpawner()
    {
        _ennemiesSpawned = 0;
        ennemyCount += 5;
        ennemyCountToKill = ennemyCount;
        OnUpdateWaveEnnemies?.Invoke(this);
    }

    public int GetEnnemiesSpawned()
    {
        return _ennemiesSpawned;
    }

    public int GetEnnemyCount()
    {
        return ennemyCount;
    }
}
