using System;
using _Project.Scripts.Scriptables;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Characters.PlayableCharacters
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterStatsSO;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerWeaponManager _playerWeaponManager;

        private float _speed;
        private float _damageBase;
        private float _maxHealth;
        
        private IDamagable _target;
        private int _maxRange;

        private void Awake()
        {
            _damageBase = _characterStatsSO.DamageBase;
            _maxHealth = _characterStatsSO.MaxHealth;

            _playerController.MovementSpeed = _characterStatsSO.Speed;
            _playerHealth.ResetHealth(_characterStatsSO.MaxHealth);
            _playerWeaponManager.ResetVisionRange(_characterStatsSO.VisionRange);
        }
    }
}
