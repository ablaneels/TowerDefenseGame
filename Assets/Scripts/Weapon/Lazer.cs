using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] private float minDistanceToDealDanage = 0.1f;

    public WeaponProjectile WeaponOwner { get; set; }
    public float Damage { get; set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Enemy>()._enemyHealth.DealDamage(Damage);
    }

    public void TurnOffAnimation()
    {
        GetComponent<Animator>().enabled = false;
    }
}
