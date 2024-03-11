using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action onWeaponSold;

    [SerializeField] private GameObject attackRangeSprite;

    public Weapon Weapon { get; set; }

    private float _rangeSize;
    private Vector3 _rangeOriginalSize;

    void Start()
    {
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize = attackRangeSprite.transform.localScale;
    }

    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    public bool IsEmpty()
    {
        return Weapon == null;
    }

    public void CloseAttackRangeSprite()
    {
        attackRangeSprite.SetActive(false);
    }

    public void SelectWeapon()
    {
        OnNodeSelected?.Invoke(this);
        if (IsEmpty())
        {
            ShowWeaponInfo();
        }
    }

    public void SellWeapon()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Weapon.WeaponUpgrade.GetSellValue());
            Destroy(Weapon.gameObject);
            Weapon = null;
            attackRangeSprite.SetActive(false);
            onWeaponSold?.Invoke();
        }
    }

    public void ShowWeaponInfo()
    {
        UIManager.Instance.SetNode(this);
        UIManager.Instance.OpenWeaponShopPanel();
    }
}
