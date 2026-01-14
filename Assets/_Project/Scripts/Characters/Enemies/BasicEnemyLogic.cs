using System;
using _Project.Scripts.Scriptables;
using UnityEngine;

namespace _Project.Scripts.Characters.Enemies
{
    [RequireComponent(typeof(CharacterController))]
    public class BasicEnemyScript : MonoBehaviour, IDamagable, IAttacker
    {
        public EnemyData Data;
        public PlayerPositionSO TargetPositionSO;
        public PoolableItem Item;

        private Vector3 _playerDirection;
        private CharacterController _controller;

        private float _speed;
        private float _damage;
        private int _maxHealth;
        private int _health;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_controller != null)
            {
                _controller.Move(_playerDirection * _speed * Time.deltaTime);
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

        public void TakeDamage(int damage)
        {
            if (_maxHealth > 0)
            {
                _health -= damage;
                if (_health <= 0)
                {
                    _health = 0;
                    Item.Kill();
                }
            }
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}