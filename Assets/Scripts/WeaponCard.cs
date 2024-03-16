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

    public void SelectWeapon()
    {
        UIManager.Instance.SetWeaponCard(this);
        GridManager.OnWeaponIsSelected?.Invoke(true);
    }

    public void PlaceWeapon()
    {
        if (CurrencySystem.Instance.TotalCoins >= WeaponLoaded.WeaponShopCost)
        {
            CurrencySystem.Instance.RemoveCoins(WeaponLoaded.WeaponShopCost);
            Instantiate(weapon, UIManager.Instance.Node.transform);
            UIManager.Instance.WeaponCard = null;
            UIManager.Instance.Node = null;

        }
        GridManager.OnWeaponIsSelected?.Invoke(false);
    }
}
