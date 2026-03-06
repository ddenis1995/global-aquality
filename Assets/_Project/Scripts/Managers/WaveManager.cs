using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.Scriptables.LevelSequencing;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Managers
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Level & Pools")] [SerializeField]
        private LevelSequence _levelConfig;

        [SerializeField] private Transform[] _defaultSpawnPoints;

        [Header("Performance tuning")] [SerializeField]
        private int _defaultPoolCapacity = 20;

        [SerializeField] private int _defaultMaxPoolSize = 100;

        private int _currentWaveIndex = -1;
        private int _enemiesAlive = 0;

        private Dictionary<Enemy, IObjectPool<Enemy>> _pools
            = new Dictionary<Enemy, IObjectPool<Enemy>>();

        private void Awake()
        {
            if (_levelConfig == null || _levelConfig.Waves == null || _levelConfig.Waves.Length == 0)
            {
                Debug.LogError("No level config or no waves assigned!");
                return;
            }

            // Create pools for every unique prefab across all waves
            HashSet<Enemy> uniqueEnemies = new HashSet<Enemy>();
            foreach (var wave in _levelConfig.Waves)
            {
                foreach (var entry in wave.EnemiesToSpawn)
                {
                    if (entry.EnemyPrefab != null)
                        uniqueEnemies.Add(entry.EnemyPrefab);
                }
            }

            foreach (var enemy in uniqueEnemies)
            {
                _pools[enemy] = new ObjectPool<Enemy>(
                    createFunc: () => Instantiate(enemy),
                    actionOnGet: obj => obj.SetActive(true),
                    actionOnRelease: obj => obj.SetActive(false),
                    actionOnDestroy: obj => Destroy(obj),
                    collectionCheck: false,
                    defaultCapacity: _defaultPoolCapacity,
                    maxSize: _defaultMaxPoolSize
                );
            }
        }

        private void Start()
        {
            StartCoroutine(LevelRoutine());
        }

        private IEnumerator LevelRoutine()
        {
            foreach (var waveConfig in _levelConfig.Waves)
            {
                _currentWaveIndex++;
                _enemiesAlive = 0;

                // Optional UI event → "Wave {currentWaveIndex + 1} starting!"
                Debug.Log($"Starting wave {_currentWaveIndex + 1} / {_levelConfig.Waves.Length}");

                yield return new WaitForSeconds(waveConfig.DelayBeforeWave);

                yield return StartCoroutine(SpawnWaveRoutine(waveConfig));

                // Wait until wave is fully cleared
                yield return new WaitUntil(() => _enemiesAlive <= 0);
            }

            // Level complete
            yield return new WaitForSeconds(_levelConfig.TimeAfterLastWave);
            Debug.Log("Level complete!");
            // → Trigger win screen, load next level, etc.
        }

        private IEnumerator SpawnWaveRoutine(WaveData wave)
        {
            // Reset per-wave counters
            foreach (var entry in wave.EnemiesToSpawn)
                entry.SpawnedSoFar = 0;

            int totalToSpawn = 0;
            foreach (var e in wave.EnemiesToSpawn) totalToSpawn += e.Count;

            int spawned = 0;

            while (spawned < totalToSpawn)
            {
                var spawnData = PickNextEnemy(wave.EnemiesToSpawn);
                if (spawnData == null) break;

                Transform spawnPoint = PickSpawnPoint(wave.CustomSpawnPoints);

                var instance = _pools[spawnData.EnemyPrefab].Get();
                instance.transform.position = spawnPoint.position;
                instance.transform.rotation = spawnPoint.rotation;

                var enemyComp = instance.GetComponent<Enemy>(); // your enemy base class
                if (enemyComp != null)
                {
                    enemyComp.OnDeathCallback = () =>
                    {
                        _enemiesAlive--;
                        _pools[spawnData.EnemyPrefab].Release(instance);
                    };
                }

                _enemiesAlive++;
                spawned++;
                spawnData.SpawnedSoFar++;

                float delay = wave.TimeBetweenSpawns;
                if (wave.SpawnTimeVariance > 0.01f)
                    delay += Random.Range(-wave.SpawnTimeVariance, wave.SpawnTimeVariance);

                yield return new WaitForSeconds(Mathf.Max(0.05f, delay));
            }
        }

        private WaveData.EnemySpawnData PickNextEnemy(List<WaveData.EnemySpawnData> list)
        {
            // Simple version: first available with count remaining
            foreach (var entry in list)
            {
                if (entry.SpawnedSoFar < entry.Count)
                    return entry;
            }

            return null;

            // Alternative: weighted random among remaining
            // float totalWeight = 0;
            // foreach (var e in list) if (e.spawnedSoFar < e.count) totalWeight += e.weight;
            // ... then pick weighted ...
        }

        private Transform PickSpawnPoint(Transform[] customPoints)
        {
            var points = customPoints != null && customPoints.Length > 0
                ? customPoints
                : _defaultSpawnPoints;

            if (points == null || points.Length == 0)
                return transform; // fallback to spawner position

            return points[Random.Range(0, points.Length)];
        }

        // Call this from enemy death / inspector / UI
        public void StopAllSpawning()
        {
            StopAllCoroutines();
        }
    }
}