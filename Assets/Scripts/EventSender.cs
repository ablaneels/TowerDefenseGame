using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    private int _ennemiesRemaining = 0;
 
    public event OnVariableChangeDelegate OnVariableChange;
    public delegate void OnVariableChangeDelegate(int newVal);
 
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
            if (OnVariableChange != null)
                OnVariableChange(_ennemiesRemaining);
        }
    }
}
