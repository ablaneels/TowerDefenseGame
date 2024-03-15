using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Tilemap tilemap;

    private void Start()
    {
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
}
