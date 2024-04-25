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
        var currentMap = PlayerPrefs.GetString("Map");
        int weaponCount;

        if ("Map1" == currentMap)
            weaponCount = 1;
        else if ("Map2" == currentMap)
            weaponCount = 2;
        else
            weaponCount = weapons.Count;
            
        for (int i = 0; i < weaponCount; i++)
        {
            GameObject currentInstance = Instantiate(weaponCardPrefab, weaponPanelContainer);
            currentInstance.GetComponent<WeaponCard>().SetUpWeaponButton(weapons[i]);
        }
    }
}
