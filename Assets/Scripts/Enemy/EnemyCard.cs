using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCard : MonoBehaviour
{
    public EnemiesPooler enemiesPooler;
    public float health;
    public float speed;

    public void OnEnterOverEnemy()
    {
        if (gameObject.name == "Enemy1")
        {
            health = enemiesPooler.enemy1Health;
            speed = enemiesPooler.enemy1MoveSpeed;
        }
        if (gameObject.name == "Enemy2")
        {
            health = enemiesPooler.enemy2Health;
            speed = enemiesPooler.enemy2MoveSpeed;
        }
        if (gameObject.name == "Enemy3")
        {
            health = enemiesPooler.enemy3Health;
            speed = enemiesPooler.enemy3MoveSpeed;
        }
        UIManager.Instance.enemyInfo.GetComponent<EnemyInfo>().enemyName.text = gameObject.name;
        UIManager.Instance.enemyInfo.GetComponent<EnemyInfo>().hp.text = "HP: " + health.ToString();
        UIManager.Instance.enemyInfo.GetComponent<EnemyInfo>().spd.text = "SPD: " + speed.ToString();
        UIManager.Instance.enemyInfo.SetActive(true);
    }

    public void OnExitOverEnemy()
    {
        UIManager.Instance.enemyInfo.SetActive(false);
    }
}