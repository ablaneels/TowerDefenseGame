using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemiesPooler : MonoBehaviour
{
    public LevelManager levelManager;

    [SerializeField] private List<GameObject> prefab;
    [SerializeField] private int poolSize;
    private List<GameObject> _pool;
    private GameObject _poolContainer;
    private int enemyType;

    public int enemy1Health;
    public int enemy2Health;
    public int enemy3Health;
    public float enemy1MoveSpeed;
    public float enemy2MoveSpeed;
    public float enemy3MoveSpeed;

    private void Awake()
    {
        enemy1Health = 60;
        enemy2Health = 90;
        enemy3Health = 120;
        enemy1MoveSpeed = 2f;
        enemy2MoveSpeed = 1.5f;
        enemy3MoveSpeed = 1f;
        InitPooler();
        //CreatePooler();
    }

    public void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (levelManager.CurrentWave == 1)
                enemyType = 0;
            else if (levelManager.CurrentWave == 2)
            {
                if (i < poolSize / 2)
                    enemyType = 1;
                else
                    enemyType = 0;
            }
            else
            {
                if (i < poolSize / 3)
                    enemyType = 2;
                else if (i < 2 * (poolSize / 3))
                    enemyType = 1;
                else
                    enemyType = 0;
            }
            _pool.Add(CreateInstance());
        }
    }

    public void InitPooler()
    {
        if (levelManager.CurrentWave > 1)
            poolSize += 5;
        else
            poolSize = 10;
        _pool = new List<GameObject>();
        if (_poolContainer == null)
            _poolContainer = new GameObject($"Pool - Enemy");
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab[enemyType]);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        if (levelManager.CurrentWave > 3)
        {
            if (newInstance.GetComponent<EnemyHealth>().initialHealth == 60)
            {
                newInstance.GetComponent<EnemyHealth>().initialHealth = enemy1Health;
                newInstance.GetComponent<EnemyHealth>().maxHealth = enemy1Health;
                newInstance.GetComponent<Enemy>().MoveSpeed = enemy1MoveSpeed;
            }
            else if (newInstance.GetComponent<EnemyHealth>().initialHealth == 90)
            {
                newInstance.GetComponent<EnemyHealth>().initialHealth = enemy2Health;
                newInstance.GetComponent<EnemyHealth>().maxHealth = enemy2Health;
                newInstance.GetComponent<Enemy>().MoveSpeed = enemy2MoveSpeed;
            }
            else if (newInstance.GetComponent<EnemyHealth>().initialHealth == 120)
            {
                newInstance.GetComponent<EnemyHealth>().initialHealth = enemy3Health;
                newInstance.GetComponent<EnemyHealth>().maxHealth = enemy3Health;
                newInstance.GetComponent<Enemy>().MoveSpeed = enemy3MoveSpeed;
            }

        }
        return newInstance;
    }

    public GameObject GetInstanceFromPool(int next)
    {
        for (int i = next; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return CreateInstance();
    }

    public static void ReturnToPool(GameObject instance)
    {
        EventSender _eventSender;
        _eventSender = FindObjectOfType<EventSender>();
        Destroy(instance);
        _eventSender.ennemiesRemaining--;
    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        EventSender _eventSender;
        _eventSender = GameObject.FindObjectOfType<EventSender>();

        yield return new WaitForSeconds(delay);
        Destroy(instance);
        _eventSender.ennemiesRemaining--;
    }
}