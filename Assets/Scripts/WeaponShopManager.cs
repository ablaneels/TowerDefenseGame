using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopManager : MonoBehaviour
{
    public GameObject weaponCardPrefab;
    public RectTransform weaponPanelContainer;
    public List<WeaponSettings> weapons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject currentInstance = Instantiate(weaponCardPrefab, weaponPanelContainer);
            currentInstance.GetComponent<WeaponCard>().SetUpWeaponButton(weapons[i]);
        }
    }
}
