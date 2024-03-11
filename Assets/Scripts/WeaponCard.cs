using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponCost;
    private GameObject weapon;

    public WeaponSettings WeaponLoaded { get; set; }

    public void SetUpWeaponButton(WeaponSettings weaponSettings)
    {
        WeaponLoaded = weaponSettings;
        weaponImage.sprite = weaponSettings.WeaponShopSprite;
        weaponCost.text = weaponSettings.WeaponShopCost.ToString();
        weapon = weaponSettings.WeaponPrefab;
    }

    public void PlaceWeapon()
    {
        Debug.Log("hello 0");
        if (CurrencySystem.Instance.TotalCoins >= WeaponLoaded.WeaponShopCost)
        {
            Debug.Log("hello 1");
            CurrencySystem.Instance.RemoveCoins(WeaponLoaded.WeaponShopCost);
            Debug.Log("hello 2");
            UIManager.Instance.CloseWeaponShopPanel();
            Instantiate(weapon, UIManager.Instance.Node.transform);
        }
    }
}
