using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;

    public Waypoint CurrentWayPoint;
    public Spawner spawner;
    public EnemiesPooler enemiesPooler;

    public int TotalLives;
    public int CurrentWave;

    // Start is called before the first frame update
    void Awake()
    {
        TotalLives = lives;
        CurrentWave = 1;
        enemiesPooler.CreatePooler();
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
            Debug.Log("WAVECOMPLETED");
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
