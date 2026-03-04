using System.Collections.Generic;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.TestEnemyPooling;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class EnemyManager:MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        [SerializeField] private MeleeEnemy meleePrefab;
        [SerializeField] private RangedEnemy rangedPrefab;
        [SerializeField] private int initialPoolSize = 10;

        private EnemyPool<MeleeEnemy> meleePool;
        private EnemyPool<RangedEnemy> rangedPool;
        private List<Enemy> activeEnemies = new List<Enemy>(); // Shared list of all active enemies

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            // Initialize pools (add more for other types)
            meleePool = new EnemyPool<MeleeEnemy>(meleePrefab, initialPoolSize, transform);
            rangedPool = new EnemyPool<RangedEnemy>(rangedPrefab, initialPoolSize, transform);
        }

        public void SpawnEnemy(EnemyType type, Vector3 position, Quaternion rotation = default)
        {
            Enemy enemy = null;
            switch (type)
            {
                case EnemyType.Melee:
                    enemy = meleePool.Get(position, rotation);
                    break;
                case EnemyType.Ranged:
                    enemy = rangedPool.Get(position, rotation);
                    break;
                // Add cases for more types
            }
            if (enemy != null)
            {
                activeEnemies.Add(enemy);
            }
        }

        public void DespawnEnemy(Enemy enemy)
        {
            activeEnemies.Remove(enemy);
            if (enemy is MeleeEnemy melee) meleePool.Release(melee);
            else if (enemy is RangedEnemy ranged) rangedPool.Release(ranged);
            // Add for more types
        }

        public List<Enemy> GetActiveEnemies()
        {
            return activeEnemies;
        }
    }
    public enum EnemyType { Melee, Ranged /* Add more */ }
}