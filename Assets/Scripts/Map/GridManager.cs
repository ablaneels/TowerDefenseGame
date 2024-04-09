using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Tilemap tilemap;

    public static Action<bool> OnWeaponIsSelected;
    public static bool TileIsAvailable;
    private EventSender _eventSender;

    private void Start()
    {
        TileIsAvailable = true;
        _eventSender = transform.GetComponent<EventSender>();
        _eventSender.weaponIsSelected = false;
        OnWeaponIsSelected.Invoke(false);
        GenerateGrid();
    }

    void GenerateGrid()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                var spwanedTile = Instantiate(_tilePrefab, new Vector3(y - 15 , x - 8), Quaternion.identity, this.transform);
                spwanedTile.name = $"Tile {x} {y}";
                spwanedTile.Init(tile);
            }
        }
    }

    private void WeaponIsSelected(bool newValue)
    {
        _eventSender.weaponIsSelected = newValue;
    }

    private void OnEnable()
    {
        OnWeaponIsSelected += WeaponIsSelected;
    }

    private void OnDisable()
    {
        OnWeaponIsSelected -= WeaponIsSelected;
    }
}
