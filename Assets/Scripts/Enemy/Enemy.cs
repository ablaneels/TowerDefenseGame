using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    public float MoveSpeed;
    public int DeathCoinReward;
    public Waypoint Waypoint;

    private Vector3 CurrentPointPosition;
    private Vector3 _lastPointPosition;
    
    private SpriteRenderer _spriteRenderer;

    private int _currentWaypointIndex;
    public EnemyHealth _enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        CurrentPointPosition = Waypoint.Points[1];
        transform.position = Waypoint.Points[0];
        _lastPointPosition = Waypoint.Points[Waypoint.Points.Length - 1];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
            CurrentPointPosition = Waypoint.Points[_currentWaypointIndex];
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        EnemiesPooler.ReturnToPool(gameObject);
    }
}
