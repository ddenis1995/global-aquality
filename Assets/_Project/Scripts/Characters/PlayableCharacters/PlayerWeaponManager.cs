using System;
using _Project.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Characters.PlayableCharacters
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        [Header("Weapons")] [SerializeField]
        private WeaponSO[] _weapons; // ScriptableObjects: damage, proj prefab, etc.
        [SerializeField] private Transform _muzzlePoint;
        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private int _startingWeapon = 0;

        private float _visionRange;

        [Header("Runtime State")] [SerializeField]
        private Cooldown[] _cooldowns; // Size = weapons.Length

        private int _currentWeaponIndex = 0;
        
        [Header("Pools")]
        [SerializeField] private int poolSize = 50;  // Per weapon

        private ObjectPool<GameObject>[] _pools;

        void Awake()
        {
            if (_muzzlePoint == null)
            {
                _muzzlePoint = transform;
            }
            
            _cooldowns = new Cooldown[_weapons.Length]; // Auto-size
            for (int i = 0; i < _weapons.Length; i++)
            {
                _cooldowns[i] = new Cooldown { Duration = _weapons[i].RateOfFire }; // Init from data
            }
            
            
            _pools = new ObjectPool<GameObject>[_weapons.Length];
            for (int i = 0; i < _weapons.Length; i++)
            {
                var prefab = _weapons[i].ProjectilePrefab;
                _pools[i] = new ObjectPool<GameObject>(
                    () => Instantiate(prefab),  // Create
                    go => go.SetActive(true),   // Get
                    go => go.SetActive(false),  // Release
                    go => Destroy(go),          // Destroy if full
                    false, poolSize, 10         // Pre-warm 10
                );
            }
        }

        void Update()
        {
            Fire();
        }

        void Fire()
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_cooldowns[i].IsReady)
                {
                    var weapon = _weapons[i];
                    
                    var target = _targetFinder.FindTarget(transform.position, _visionRange, weapon.TargetingType);
                    if (target == null) continue;
                    
                    var dir = target.transform.position - transform.position;
                    
                    var pool = _pools[i];
                    var proj = pool.Get();
                    proj.transform.SetPositionAndRotation(_muzzlePoint.position, _muzzlePoint.rotation);  // Assign muzzle child
                    
                    proj.GetComponent<Projectile>().Launch(dir, weapon.Damage, weapon.Speed, pool);
                    
                    _cooldowns[i].Trigger();
                }
            }
        }

        // Public for UI
        public Cooldown GetCooldown(int index) => _cooldowns[index];

        public void ResetVisionRange(float range)
        {
            _visionRange = range;
        }
    }
}