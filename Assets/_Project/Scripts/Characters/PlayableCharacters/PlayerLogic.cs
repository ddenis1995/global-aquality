using System;
using _Project.Scripts.Scriptables;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Characters.PlayableCharacters
{
    public class PlayerLogic : MonoBehaviour, IDamagable, IAttacker
    {
        [SerializeField] private CharacterData _characterStatsSO;

        private int _speed;
        private int _damageBase;
        private int _maxHealth;
        private WeaponSO[] _weapons;
        
        private IDamagable _target;
        private int _maxRange;

        private void Start()
        {
            _speed = _characterStatsSO.Speed;
            _damageBase = _characterStatsSO.DamageBase;
            _maxHealth = _characterStatsSO.MaxHealth;
            _weapons = _characterStatsSO.Weapons;
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
            throw new System.NotImplementedException();
        }

        public void Attack()
        {
            FindTarget();
            
        }
    }
}
