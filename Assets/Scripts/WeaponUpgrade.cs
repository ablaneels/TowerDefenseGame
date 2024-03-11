using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;
    [Header ("Sell")]
    [Range(0,1)]
    [SerializeField] private float sellPert;
    public float sellPerc { get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    private WeaponProjectile _weaponProjectile;

    // Start is called before the first frame update
    void Start()
    {
        _weaponProjectile = GetComponent<WeaponProjectile>();
        UpgradeCost = upgradeInitialCost;

        sellPerc = sellPert;
        Level = 1;
    }

    public void UpgradeWeapon()
    {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
            _weaponProjectile.Damage += damageIncremental;
            _weaponProjectile.DelayPerShot -= delayReduce;
            UpdateUpgrade();
        }
    }

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * sellPerc);
        return sellValue;
    }

    public void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level ++;
    }
}
