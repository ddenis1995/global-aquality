using System;
using _Project.Scripts.Scriptables;
using UnityEngine;

namespace _Project.Scripts.Characters.Enemies
{
    [RequireComponent(typeof(CharacterController))]
    public class BasicEnemyScript : MonoBehaviour, IDamagable, IAttacker
    {
        [SerializeField] private CharacterData Data;
        [SerializeField] private PlayerPositionSO TargetPositionSO;
        [SerializeField] private PoolableItem Item;
        [SerializeField] private CharacterController _controller;

        private Vector3 _playerDirection;
        private float _speed;
        private float _damage;
        private int _maxHealth;
        private int _health;

        private void Start()
        {
            _speed = Data.Speed;
            _damage = Data.DamageBase;
            _maxHealth = Data.MaxHealth;
            _health = _maxHealth;
        }

        private void Update()
        {
            _controller.Move(_playerDirection * _speed * Time.deltaTime);
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