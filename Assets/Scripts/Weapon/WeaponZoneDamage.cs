using UnityEngine;

public class WeaponZoneDamage : MonoBehaviour
{

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    protected float _nextAttackTime;
    protected Weapon _weapon;

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<Weapon>();

        Damage = _weapon.GetDamage();
        DelayPerShot = _weapon.GetDelayBtwAttacks();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Time.time > _nextAttackTime && !LevelManager.EndOfGame && !LevelManager.PauseGame)
        {
            if (_weapon.CurrentEnemyTarget != null && _weapon.CurrentEnemyTarget._enemyHealth.CurrentHealth > 0f)
            {
                for (int i = 0; i < _weapon._enemies.Count; i++)
                {
                    _weapon._enemies[i]._enemyHealth.DealDamage(Damage);
                }
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }
}
