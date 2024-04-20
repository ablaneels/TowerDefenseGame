using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaserDamage : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;

    public float DelayPerShot { get; set; }

    protected float _nextAttackTime;
    protected Weapon _weapon;
    protected ObjectPooler _pooler;
    public GameObject lazer;

    // Start is called before the first frame update
    void Start()
    {

        _weapon = GetComponent<Weapon>();
        _pooler = GetComponent<ObjectPooler>();

        lazer.GetComponent<Lazer>().Damage = _weapon.GetDamage();
        DelayPerShot = _weapon.GetDelayBtwAttacks();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_weapon.CurrentEnemyTarget != null)
            {
                lazer.GetComponent<Animator>().enabled = true;
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }
}