using _Project.Scripts.Characters.Enemies;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.TestEnemyPooling
{
    public class EnemyPool<T> where T : Enemy
    {
        private ObjectPool<T> _pool;
        private T _prefab;
        private Transform _parent;

        public EnemyPool(T prefab, int initialSize, Transform parent = null)
        {
            this._prefab = prefab;
            this._parent = parent;
            _pool = new ObjectPool<T>(
                createFunc: CreateEnemy,
                actionOnGet: OnGetEnemy,
                actionOnRelease: OnReleaseEnemy,
                actionOnDestroy: OnDestroyEnemy,
                maxSize: 50 // Adjust based on needs
            );
            
            // Pre-warm the pool
            for (int i = 0; i < initialSize; i++)
            {
                T enemy = _pool.Get();
                _pool.Release(enemy);
            }
        }

        private T CreateEnemy()
        {
            T enemy = Object.Instantiate(_prefab, _parent);
            return enemy;
        }

        private void OnGetEnemy(T enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.Initialize();
        }

        private void OnReleaseEnemy(T enemy)
        {
            enemy.Deactivate();
        }

        private void OnDestroyEnemy(T enemy)
        {
            Object.Destroy(enemy.gameObject);
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            T enemy = _pool.Get();
            enemy.transform.position = position;
            enemy.transform.rotation = rotation;
            return enemy;
        }

        public void Release(T enemy)
        {
            _pool.Release(enemy);
        }
    }
}