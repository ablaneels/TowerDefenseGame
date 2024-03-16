using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlightGreen;
    [SerializeField] private GameObject _highlightRed;
    [SerializeField] private string _tilename;
    private bool IsAvailable;
    private EventSender _eventSender;
    private bool weaponIsSelected;

    public void Init(TileBase tile)
    {
        _tilename = tile.name;
        if (_tilename == "ClassicRPG_Sheet_44")
            IsAvailable = true;
        else
            IsAvailable = false;
        _eventSender = transform.parent.GetComponent<EventSender>();
        _eventSender.OnWeaponSelectedChange += VariableChangeHandler;
    }

    private void OnMouseEnter() {
        if (weaponIsSelected)
        {
            if (IsAvailable)
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
    }

    private void OnMouseExit() {
        _highlightGreen.SetActive(false);
        _highlightRed.SetActive(false);
    }

    private void VariableChangeHandler(bool newVal)
    {
        weaponIsSelected = newVal;
    }
}
