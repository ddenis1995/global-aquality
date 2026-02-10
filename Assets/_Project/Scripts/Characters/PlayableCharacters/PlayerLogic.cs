using System;
using _Project.Scripts.Scriptables;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Characters.PlayableCharacters
{
    public class PlayerLogic : MonoBehaviour, IDamagable, IAttacker
    {
        [SerializeField] private CharacterData _characterStatsSO;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerController _playerController;

        private int _speed;
        private int _damageBase;
        private int _maxHealth;
        private WeaponSO[] _weapons;
        
        private IDamagable _target;
        private int _maxRange;

        private void Awake()
        {
            _damageBase = _characterStatsSO.DamageBase;
            _maxHealth = _characterStatsSO.MaxHealth;
            _weapons = _characterStatsSO.Weapons;

            _playerController.MovementSpeed = _characterStatsSO.Speed;
            _playerHealth.ResetHealth(_characterStatsSO.MaxHealth);
        }


        private void Update()
        {
            Attack();
        }

        private void FindTarget()
        {
            
        }

        public void TakeDamage(int damage)
        {
            _playerHealth.TakeDamage(damage);
        }

        public void Attack()
        {
            FindTarget();
            
        }
    }
}
