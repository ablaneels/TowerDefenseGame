using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private int ennemyCount = 10;
    [SerializeField] private GameObject testGo;

    [Header ("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    public event OnVariableChangeDelegate OnVariableChange;
    public delegate void OnVariableChangeDelegate(int newVal);

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
        _eventSender.OnVariableChange += VariableChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_ennemiesSpawned < ennemyCount)
            {
                _ennemiesSpawned++;
                _eventSender.ennemiesRemaining++;
                SpawnEnnemy();
            }
        }
    }

    private void SpawnEnnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.GetComponent<Enemy>().Waypoint = levelManager.CurrentWayPoint;
        newInstance.SetActive(true);
    }

    private void VariableChangeHandler(int newVal)
    {
        if (newVal <= 0)
        {
            levelManager.WaveCompleted();
        }
    }
}
