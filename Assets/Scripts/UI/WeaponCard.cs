using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image weaponFilter;
    [SerializeField] private TextMeshProUGUI weaponCost;
    private GameObject weapon;

    public WeaponSettings WeaponLoaded { get; set; }

    public void SetUpWeaponButton(WeaponSettings weaponSettings)
    {
        WeaponLoaded = weaponSettings;
        weaponImage.sprite = weaponSettings.WeaponShopSprite;
        weaponImage.color = weaponSettings.color;
        weaponCost.text = weaponSettings.WeaponShopCost.ToString();
        weapon = weaponSettings.WeaponPrefab;
    }

    public void OnEnterOverWeapon()
    {
        UIManager.Instance.weaponInfo.GetComponent<WeaponInfo>().weaponName.text = weapon.name;
        UIManager.Instance.weaponInfo.GetComponent<WeaponInfo>().lvl.text = "LVL: 0";
        UIManager.Instance.weaponInfo.GetComponent<WeaponInfo>().spd.text = "SPD: " + weapon.GetComponent<Weapon>().GetDelayBtwAttacks().ToString();
        UIManager.Instance.weaponInfo.GetComponent<WeaponInfo>().atk.text = "ATK: " + weapon.GetComponent<Weapon>().GetDamage().ToString();
        UIManager.Instance.weaponInfo.SetActive(true);
    }

    public void OnExitOverWeapon()
    {
        UIManager.Instance.weaponInfo.SetActive(false);
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
            var CurrentWeapon = Instantiate(weapon, UIManager.Instance.Node.transform);
            UIManager.Instance.Node.transform.GetComponent<Tile>().weapon = CurrentWeapon.GetComponent<Weapon>();
            UIManager.Instance.Node.transform.GetComponent<Tile>().SetIsAvailable(false);
            if (CurrentWeapon.name.Contains("Weapon_1"))
                CurrentWeapon.GetComponent<Weapon>().Weapon1Position(UIManager.Instance.Node.transform.GetComponent<Tile>());
            UIManager.Instance.WeaponCard = null;
            UIManager.Instance.Node = null;

        }
        GridManager.OnWeaponIsSelected?.Invoke(false);
    }

    public void UpdateMoney(int newValue)
    {
        if (Convert.ToInt32(weaponCost.text) > newValue)
        {
            weaponFilter.color = new Color32(0, 0, 0, 180);
            GetComponent<Button>().enabled = false;
        }
        else
        {
            weaponFilter.color = new Color32(0, 0, 0, 0);
            GetComponent<Button>().enabled = true;
        }
    }

    private void UpdateMoney(CurrencySystem currencySystem)
    {
        UpdateMoney(currencySystem.TotalCoins);
    }

    private void OnEnable()
    {
        CurrencySystem.OnUpdateUIMoney += UpdateMoney;
    }

    private void OnDisable()
    {
        CurrencySystem.OnUpdateUIMoney  -= UpdateMoney;
    }
}
