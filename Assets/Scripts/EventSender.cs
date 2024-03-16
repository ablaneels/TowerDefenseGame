using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    private int _ennemiesRemaining = 0;
    private bool _weaponIsSelected = false;
 
    public event OnEnnemiesRemainingDelegate OnEnnemiesRemainingChange;
    public delegate void OnEnnemiesRemainingDelegate(int newVal);

    public event OnWeaponSelectedChangeDelegate OnWeaponSelectedChange;
    public delegate void OnWeaponSelectedChangeDelegate(bool newVal);
 
    public int ennemiesRemaining
    {
        get
        {
            return _ennemiesRemaining;
        }
        set
        {
            if (_ennemiesRemaining == value) return;
            _ennemiesRemaining = value;
            if (OnEnnemiesRemainingChange != null)
                OnEnnemiesRemainingChange(_ennemiesRemaining);
        }
    }

    public bool weaponIsSelected
    {
        get
        {
            return _weaponIsSelected;
        }
        set
        {
            if (_weaponIsSelected == value) return;
            _weaponIsSelected = value;
            if (OnWeaponSelectedChange != null)
                OnWeaponSelectedChange(_weaponIsSelected);
        }
    }
}
