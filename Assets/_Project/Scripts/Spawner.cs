using _Project.Scripts.Scriptables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float EnemiesPerSecond = 1;
        [SerializeField] private PlayerPositionSO _playerPositionSO;
        [SerializeField] private float _minSpawnRadius;
        [SerializeField] private float _maxSpawnRadius;
        [SerializeField] private MyPool _myPool;


        private float LastSpawnTime;

        private void Update()
        {
            float delay = 1f / EnemiesPerSecond;
            if (LastSpawnTime + delay < Time.time)
            {
                int enemiesToSpawnInFrame = Mathf.CeilToInt(Time.deltaTime / delay);
                while (enemiesToSpawnInFrame > 0)
                {
                    _myPool.SpawnObject(CalculatePosition(_playerPositionSO.Value));

                    enemiesToSpawnInFrame--;
                }

                LastSpawnTime = Time.time;
            }
        }

        private Vector3 CalculatePosition(Vector3 startPos)
        {
            float randomDistance = Random.Range(_minSpawnRadius, _maxSpawnRadius);

            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            float x = Mathf.Cos(randomAngle) * randomDistance;
            float z = Mathf.Sin(randomAngle) * randomDistance;
        
            Vector3 pos = new Vector3(x, 0, z) + startPos;
        
            return pos;
        }
    }
}