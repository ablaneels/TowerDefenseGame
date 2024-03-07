using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private int ennemyCount = 10;
    [SerializeField] private GameObject testGo;

    [Header ("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    public int ennemiesRemaining;

    private float _spawnTimer;
    private int _ennemiesSpawned;

    private ObjectPooler _pooler;

    // Start is called before the first frame update
    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
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
                ennemiesRemaining++;
                SpawnEnnemy();
            }
        }
    }

    private void SpawnEnnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.GetComponent<Enemy>().Waypoint = GetComponent<Waypoint>();
        newInstance.SetActive(true);
    }
}
