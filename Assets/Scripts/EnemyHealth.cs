using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] public float initialHealth;
    [SerializeField] public float maxHealth;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;
    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        
        _enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }
    
    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }
    
    public void DealDamage (float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            Debug.Log("I'm dead now: ");
            CurrentHealth = 0;
            EnemiesPooler.ReturnToPool(gameObject);
            OnEnemyKilled?.Invoke(_enemy);
        }
        else
            OnEnemyHit?.Invoke(_enemy);
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }
}
