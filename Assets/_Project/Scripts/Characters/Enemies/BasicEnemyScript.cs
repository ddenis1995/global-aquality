using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicEnemyScript : MonoBehaviour
{
    public EnemyData Data;
    public PlayerPositionSO TargetPositionSO;

    private Vector3 _playerDirection;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller != null)
        {
            _controller.Move(_playerDirection * Data.Speed * Time.deltaTime);
        }
        
    }

    private void FixedUpdate()
    {
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        _playerDirection = TargetPositionSO.Value - transform.position;
        _playerDirection.Normalize();
        _playerDirection.y = 0;
    }
}