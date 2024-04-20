using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    protected float _nextAttackTime;
    protected ObjectPooler _pooler;
    protected Weapon _weapon;
    protected Projectile _currentProjectileLoaded;

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<Weapon>();
        _pooler = GetComponent<ObjectPooler>();

        Damage = _weapon.GetDamage();
        DelayPerShot = _weapon.GetDelayBtwAttacks();
        LoadProjectile();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsWeaponEmpty())
        {
            LoadProjectile();
        }
        if (Time.time > _nextAttackTime)
        {
            if (_weapon.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _weapon.CurrentEnemyTarget._enemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.SetParent(_pooler._poolContainer.transform);
                _currentProjectileLoaded.SetEnemy(_weapon.CurrentEnemyTarget);
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }

    protected virtual void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.position = projectileSpawnPosition.position;
        newInstance.transform.rotation = projectileSpawnPosition.rotation;

        newInstance.transform.SetParent(projectileSpawnPosition);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.WeaponOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.Damage = Damage;
        newInstance.SetActive(true);
    }

    public bool IsWeaponEmpty()
    {
        if (!_currentProjectileLoaded)
            return true;
        return false;
    }

    public void ResetWeaponProjectile()
    {
        LoadProjectile();
    }
}
