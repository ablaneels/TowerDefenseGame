using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject canvas;
    public TextMeshProUGUI money;
    public TextMeshProUGUI wave;

    private int currentWave;
    private int enemyType1;
    private int enemyType2;
    private int enemyType3;


    public Node Node { get; set; }
    public WeaponCard WeaponCard { get; set; }

    private void Awake() {
        if (Instance != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void UpdateMoney(int newValue)
    {
        money.text = newValue.ToString();
    }

    public void UpdateWave(int newValue)
    {
        currentWave = newValue;
        wave.text = "Wave " + currentWave.ToString();
    }

    public void UpdateWaveEnnemies(int newValue)
    {
        enemyType1 = 0;
        enemyType1 = 0;
        enemyType3 = 0;
        if (currentWave == 1)
            enemyType1 = newValue;
        else if (currentWave == 2)
        {
            enemyType1 = newValue / 2;
            enemyType2 = newValue / 2;
        }
        else
        {
            enemyType1 = newValue / 3;
            enemyType2 = newValue / 3;
            enemyType3 = newValue / 3;
        }
    }

    public void SetNode(Node node)
    {
        Node = node;
    }

    public void SetWeaponCard(WeaponCard weaponCard)
    {
        WeaponCard = weaponCard;
    }

    private void UpdateMoney(CurrencySystem currencySystem)
    {
        UpdateMoney(currencySystem.TotalCoins);
    }

    private void UpdateWave(LevelManager levelManager)
    {
        UpdateWave(levelManager.CurrentWave);
    }

    private void UpdateWaveEnnemies(Spawner spawner)
    {
        UpdateWaveEnnemies(spawner.GetEnnemiesSpawned());
    }

    private void OnEnable()
    {
        CurrencySystem.OnUpdateUIMoney += UpdateMoney;
        LevelManager.OnUpdateUILevel += UpdateWave;
        Spawner.OnUpdateWaveEnnemies += UpdateWaveEnnemies;
    }

    private void OnDisable()
    {
        CurrencySystem.OnUpdateUIMoney  -= UpdateMoney;
        LevelManager.OnUpdateUILevel -= UpdateWave;
        Spawner.OnUpdateWaveEnnemies += UpdateWaveEnnemies;
    }
}
