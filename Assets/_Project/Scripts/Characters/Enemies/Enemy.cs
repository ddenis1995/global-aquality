using System;
using _Project.Scripts.Managers;
using _Project.Scripts.Scriptables;
using UnityEngine;

namespace _Project.Scripts.Characters.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private CharacterData Data;
        [SerializeField] private PlayerPositionSO TargetPositionSO;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private LayerMask playerLayer;
        
        public System.Action OnDeathCallback;
        
        private Vector3 _playerDirection;
        private float _speed;
        private float _damage;
        private float _attackRange;
        private float _attackCooldown;
        private float _maxHealth;
        private float _health;
        private string _enemyType;
        private bool _isAttacker;
        
        private float _nextAttackTime = 0f; // Key: absolute time when next attack allowed

        private void OnEnable()
        {
            Initialize();
        }

        private void Update()
        {
            if (_isAttacker){
                // Every frame: check if cooldown ready + player in range
                if (Time.time >= _nextAttackTime)
                {
                    _controller.Move(_playerDirection * _speed * Time.deltaTime);
                    if (PlayerInRange())
                    {
                        Attack();
                        _nextAttackTime = Time.time + _attackCooldown; // Reset cooldown 
                    }
                }
            }
        }
        
        private void FixedUpdate()
        {
            CalculateDirection();
        }
        
        private bool PlayerInRange()
        {
            //return Physics.OverlapSphere(transform.position, _attackRange, playerLayer).Length > 0;
            return _attackRange >= Vector3.SqrMagnitude(TargetPositionSO.Value - transform.position);
        }
        
        private void CalculateDirection()
        {
            _playerDirection = TargetPositionSO.Value - transform.position;
            _playerDirection.Normalize();
            _playerDirection.y = 0;
        }
        
        public virtual void TakeDamage(float damage)
        {
            if (_maxHealth > 0)
            {
                //Debug.Log($"I take {damage} points of damage");
                // Hit FX/anim...
                _health -= damage;
                if (_health <= 0)
                {
                    Die();
                }
            }
        }
        
        public virtual void Die()
        {
            _health = 0;
            GameManagement.RegisterKill(_enemyType);
            // Death FX/anim...
            Deactivate();
        }
        
        public virtual void Attack()
        {
            // Hit FX/anim...
            PlayerHealth.Instance?.TakeDamage(_damage);
            //nextAttackTime = Time.time + attackCooldown;
        }
        
        public float GetHealth()
        {
            return _health;
        }
        
        // Common methods, e.g., TakeDamage(), Die(), etc.
        public virtual void Initialize()
        {
            /* Reset state when spawned */
            
            _speed = Data.Speed;

            _damage = Data.DamageBase;
            _attackRange = Data.Range;
            _attackCooldown = Data.Cooldown;

            _maxHealth = Data.MaxHealth;
            _health = _maxHealth;

            _enemyType = Data.NameOrType;
            _isAttacker = Data.IsAttacker;
        }

        public virtual void Deactivate()
        {
            OnDeathCallback?.Invoke();
        }
    }
}