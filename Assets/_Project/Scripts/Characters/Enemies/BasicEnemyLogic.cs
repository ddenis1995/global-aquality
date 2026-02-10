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
        [SerializeField] private LayerMask playerLayer;

        private Vector3 _playerDirection;
        private float _speed;
        private float _damage;
        private float _attackRange;
        private float _attackCooldown;
        private int _maxHealth;
        private int _health;

        private float nextAttackTime = 0f; // Key: absolute time when next attack allowed

        private void Start()
        {
            _speed = Data.Speed;

            _damage = Data.DamageBase;
            _attackRange = Data.Range;
            _attackCooldown = Data.Cooldown;

            _maxHealth = Data.MaxHealth;
            _health = _maxHealth;
        }

        private void Update()
        {
            // Every frame: check if cooldown ready + player in range
            if (Time.time >= nextAttackTime)
            {
                _controller.Move(_playerDirection * _speed * Time.deltaTime);
                if (PlayerInRange())
                {
                    Attack();
                    nextAttackTime = Time.time + _attackCooldown; // Reset cooldown 
                }
            }
        }

        private bool PlayerInRange()
        {
            return Physics.OverlapSphere(transform.position, _attackRange, playerLayer).Length > 0;
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
            // Hit FX/anim...
            PlayerHealth.Instance?.TakeDamage(_damage);
            //nextAttackTime = Time.time + attackCooldown;
        }
    }
}