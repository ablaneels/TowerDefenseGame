using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action onWeaponSold;

    public Weapon Weapon { get; set; }

    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    public bool IsEmpty()
    {
        return Weapon == null;
    }

    public void SelectWeapon()
    {
        OnNodeSelected?.Invoke(this);
        if (IsEmpty() && UIManager.Instance.WeaponCard != null && GridManager.TileIsAvailable)
        {
            ShowWeaponInfo();
            UIManager.Instance.WeaponCard.PlaceWeapon();
        }
    }

    public void SellWeapon()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Weapon.WeaponUpgrade.GetSellValue());
            Destroy(Weapon.gameObject);
            Weapon = null;
            onWeaponSold?.Invoke();
        }
    }

    public void ShowWeaponInfo()
    {
        UIManager.Instance.SetNode(this);
    }
}
