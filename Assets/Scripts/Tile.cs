using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlightGreen;
    [SerializeField] private GameObject _highlightRed;
    [SerializeField] private string _tilename;
    private bool isAvailable;

    public void Init(TileBase tile)
    {
        _tilename = tile.name;
        if (_tilename == "ClassicRPG_Sheet_44")
            isAvailable = true;
        else
            isAvailable = false;
    }

    private void OnMouseEnter() {
        if (isAvailable)
        {
            _highlightGreen.SetActive(true);
            _highlightRed.SetActive(false);
        }
        else
        {
            _highlightGreen.SetActive(false);
            _highlightRed.SetActive(true);
        }
    }

    void OnMouseOver()
    {
        if (isAvailable)
        {
            _highlightGreen.SetActive(true);
            _highlightRed.SetActive(false);
        }
        else
        {
            _highlightGreen.SetActive(false);
            _highlightRed.SetActive(true);
        }
    }

    private void OnMouseExit() {
        _highlightGreen.SetActive(false);
        _highlightRed.SetActive(false);
    }
}
