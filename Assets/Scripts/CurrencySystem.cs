using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class CurrencySystem : MonoBehaviour
{
    public static CurrencySystem Instance { get; private set; }

    public static Action<CurrencySystem> OnUpdateUIMoney;

    [SerializeField] private int coinTest;
    private string CURRENCY_SAVE_KEY = "GAME_CURRENCY";

    public int TotalCoins { get; set; }

    private void Awake() {
        if (Instance != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
        LoadCoins();
    }

    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY, coinTest);
        OnUpdateUIMoney?.Invoke(this);
    }

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        if (TotalCoins > 300)
            TotalCoins = 300;
        if (TotalCoins != PlayerPrefs.GetInt(CURRENCY_SAVE_KEY))
        {
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
            OnUpdateUIMoney?.Invoke(this);
        }
    }

    public void RemoveCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
            OnUpdateUIMoney?.Invoke(this);
        }
    }

    private void AddCoins(Enemy enemy)
    {
        AddCoins(enemy.DeathCoinReward);
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }
}
