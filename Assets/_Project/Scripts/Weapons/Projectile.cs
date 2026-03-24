using System;
using _Project.Scripts.Characters.Enemies;
using UnityEngine;
using UnityEngine.Pool; // Unity 2021+

namespace _Project.Scripts.Weapons
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 5f;
        private float _damage, _speed;
        private Vector3 _direction;
        private float _timer;

        private ObjectPool<GameObject> pool; // Set by spawner

        private void OnEnable()
        {
            _timer = _lifeTime;
        }

        public void Launch(Vector3 dir, float dmg, float spd, ObjectPool<GameObject> p)
        {
            _direction = dir;
            _damage = dmg;
            _speed = spd;
            pool = p;
            // Virtual: unique setup
            OnLaunch();
        }

        protected abstract void OnLaunch(); // ← Unique behavior here!

        protected virtual void Update()
        {
            transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
            
            _timer -= Time.deltaTime; // Decrease timer based on elapsed time
            if (_timer <= 0)
            {
                ReturnToPool();
            }
        }


        protected void ReturnToPool() => pool.Release(gameObject);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(_damage);
                OnHit(other); // Virtual hit FX
                ReturnToPool();
            }
        }

        protected virtual void OnHit(Collider target)
        {
            /* Default: nothing */
        }
    }
}