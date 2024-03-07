using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooler : MonoBehaviour
{
    public LevelManager levelManager;

    [SerializeField] private List<GameObject> prefab;
    [SerializeField] private int poolSize;
    private List<GameObject> _pool;
    private GameObject _poolContainer;
    private int enemyType;

    private void Awake()
    {
        poolSize = 10;
        _pool = new List<GameObject>();
        _poolContainer = new GameObject($"Pool - Enemy");
        //CreatePooler();
    }

    public void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Debug.Log(levelManager.CurrentWave);
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

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab[enemyType]);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
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
        _eventSender = GameObject.FindObjectOfType<EventSender>();

        instance.SetActive(false);
        _eventSender.ennemiesRemaining -= 1;
    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        EventSender _eventSender;
        _eventSender = GameObject.FindObjectOfType<EventSender>();

        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
        _eventSender.ennemiesRemaining -= 1;
    }
}