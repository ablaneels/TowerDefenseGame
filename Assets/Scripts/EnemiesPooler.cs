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

    private void Awake()
    {
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
        if (_poolContainer != null)
            Destroy(_poolContainer);
        _poolContainer = new GameObject($"Pool - Enemy");
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab[enemyType]);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
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