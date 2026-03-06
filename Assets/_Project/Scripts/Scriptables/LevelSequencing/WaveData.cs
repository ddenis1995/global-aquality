using System.Collections.Generic;
using _Project.Scripts.Characters.Enemies;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeBlock", menuName = "Scriptable Objects/TimeBlock")]
public class WaveData : ScriptableObject
{
    public string WaveName;
    
    [Tooltip("How long to wait before starting this wave (after previous wave ends)")]
    public float DelayBeforeWave = 3f;

    [Tooltip("Time between individual enemy spawns in this wave")]
    public float TimeBetweenSpawns = 0.4f;

    [Tooltip("Optional - random range for spawn timing variation")]
    public float SpawnTimeVariance = 0.15f;        // Time when spawning starts

    public class EnemySpawnData
    {
        public Enemy EnemyPrefab;  // Enemy type
        public int Count;               // Amount of enemies
        public float Weight = 1f;            // If weighted random order needed
        [HideInInspector] public int SpawnedSoFar = 0;
    }

    public List<EnemySpawnData> EnemiesToSpawn = new List<EnemySpawnData>();
    
    // Optional: override spawn points for this wave (otherwise uses global ones)
    public Transform[] CustomSpawnPoints;
}