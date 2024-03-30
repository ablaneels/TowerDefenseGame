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
    public GameObject nextWaveUI;

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
            enemyType1 = newValue - (newValue / 2);
            enemyType2 = newValue / 2 ;
        }
        else
        {
            enemyType1 = newValue - (2 * (newValue / 3));
            enemyType2 = newValue / 3;
            enemyType3 = newValue / 3;
        }
        Debug.Log("Enemy wave: " + newValue);
        Debug.Log("Current wave: " + currentWave);
    }

    public void UpdateNextWaveEnnemies(int newValue)
    {
        var nextWave = currentWave + 1;
        var nextValue = newValue + 5;
        enemyType1 = 0;
        enemyType1 = 0;
        enemyType3 = 0;
        if (nextWave == 2)
        {
            nextWaveUI.transform.Find("Enemy1").gameObject.SetActive(true);
            nextWaveUI.transform.Find("Enemy2").gameObject.SetActive(true);
            nextWaveUI.transform.Find("Enemy3").gameObject.SetActive(false);
            enemyType1 = nextValue - (nextValue / 2);
            enemyType2 = nextValue / 2;
            nextWaveUI.transform.Find("Enemy1").Find("Enemy Number").GetComponent<TextMeshProUGUI>().text = enemyType1.ToString();
            nextWaveUI.transform.Find("Enemy2").Find("Enemy Number").GetComponent<TextMeshProUGUI>().text = enemyType2.ToString();
        }
        else
        {
            nextWaveUI.transform.Find("Enemy1").gameObject.SetActive(true);
            nextWaveUI.transform.Find("Enemy2").gameObject.SetActive(true);
            nextWaveUI.transform.Find("Enemy3").gameObject.SetActive(true);
            enemyType1 = nextValue - (2 * (nextValue / 3));
            enemyType2 = nextValue / 3;
            enemyType3 = nextValue / 3;
            nextWaveUI.transform.Find("Enemy1").Find("Enemy Number").GetComponent<TextMeshProUGUI>().text = enemyType1.ToString();
            nextWaveUI.transform.Find("Enemy2").Find("Enemy Number").GetComponent<TextMeshProUGUI>().text = enemyType2.ToString();
            nextWaveUI.transform.Find("Enemy3").Find("Enemy Number").GetComponent<TextMeshProUGUI>().text = enemyType3.ToString();
        }

        Debug.Log("Next Enemy wave: " + nextValue);
        Debug.Log("Next wave: " + nextWave);
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
        UpdateWaveEnnemies(spawner.GetEnnemyCount());
        UpdateNextWaveEnnemies(spawner.GetEnnemyCount());
    }

    private void OnEnable()
    {
        LevelManager.OnUpdateUILevel += UpdateWave;
        CurrencySystem.OnUpdateUIMoney += UpdateMoney;
        Spawner.OnUpdateWaveEnnemies += UpdateWaveEnnemies;
    }

    private void OnDisable()
    {
        LevelManager.OnUpdateUILevel -= UpdateWave;
        CurrencySystem.OnUpdateUIMoney  -= UpdateMoney;
        Spawner.OnUpdateWaveEnnemies += UpdateWaveEnnemies;
    }
}
