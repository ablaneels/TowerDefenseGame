using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float delayBtwAttacks;
    [SerializeField] protected float damage;
    public List<Enemy> _enemies;
    public Enemy CurrentEnemyTarget;
    public WeaponUpgrade WeaponUpgrade;
    public GameObject weaponRange;
    public bool shouldRotate;

    // Start is called before the first frame update
    void Start()
    {
        _enemies = new List<Enemy>();
        WeaponUpgrade = GetComponent<WeaponUpgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }

    public float GetDelayBtwAttacks()
    {
        return delayBtwAttacks;
    }

    public void SetDelayBtwAttacks(float newValue)
    {
        delayBtwAttacks += newValue;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float newValue)
    {
        damage += newValue;
    }

    void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null || !shouldRotate)
            return;
        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void Weapon1Position(Tile tile)
    {
        var str = tile.name.Split(" ");
        var map = tile.transform.parent;

        List<int> res = new List<int>();
        List<int> resRotation = new List<int>();
        int bestRes = 0;
        
        res.Add(CheckMapDirection(0, -1, map, str));
        resRotation.Add(90);

        res.Add(CheckMapDirection(0, 1, map, str));
        resRotation.Add(-90);

        res.Add(CheckMapDirection(1, 0, map, str));
        resRotation.Add(0);

        res.Add(CheckMapDirection(-1, 0, map, str));
        resRotation.Add(180);

        res.Add(CheckMapDirection(1, -1, map, str));
        resRotation.Add(45);

        res.Add(CheckMapDirection(1, 1, map, str));
        resRotation.Add(-45);

        res.Add(CheckMapDirection(-1, -1, map, str));
        resRotation.Add(135);

        res.Add(CheckMapDirection(-1, 1, map, str));
        resRotation.Add(-135);

        for (int i = 0; i < res.Count; i++)
        {
            if (res[i] > bestRes)
                bestRes = i;
            if (res[i] == 3)
            {
                transform.Rotate(0f, 0f, resRotation[i]);
                return;
            }
        }

        transform.Rotate(0f, 0f, resRotation[bestRes]);
        return;
    }

    int CheckMapDirection(int checkX, int checkY, Transform map, string[] str)
    {
        var check = 0;
        for (int i= 1; i < 4; i++)
        {
            check += map.Find(str[0] + " " + (Convert.ToInt32(str[1]) + (i * checkX)) + " " + (Convert.ToInt32(str[2]) + (i * checkY))).GetComponent<Tile>().IsTileRoad() ? 1 : 0;
        }
        return check;
    }
}
