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

    public Node Node { get; set; }

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
        wave.text = "Wave " + newValue.ToString();
    }

    public void SetNode(Node node)
    {
        Node = node;
    }

    private void UpdateMoney(CurrencySystem currencySystem)
    {
        UpdateMoney(currencySystem.TotalCoins);
    }

    private void UpdateWave(LevelManager levelManager)
    {
        UpdateWave(levelManager.CurrentWave);
    }

    private void OnEnable()
    {
        CurrencySystem.OnUpdateUIMoney += UpdateMoney;
        LevelManager.OnUpdateUILevel += UpdateWave;
    }

    private void OnDisable()
    {
        CurrencySystem.OnUpdateUIMoney  -= UpdateMoney;
        LevelManager.OnUpdateUILevel -= UpdateWave;
    }
}
