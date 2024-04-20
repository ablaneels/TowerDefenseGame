using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 1)]
public class WeaponSettings : ScriptableObject
{
    public GameObject WeaponPrefab;
    public int WeaponShopCost;
    public Sprite WeaponShopSprite;
    public Color color;
}