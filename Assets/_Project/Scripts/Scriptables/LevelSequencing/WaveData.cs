using UnityEngine;

[CreateAssetMenu(fileName = "TimeBlock", menuName = "Scriptable Objects/TimeBlock")]
public class WaveData : ScriptableObject
{
    public string WaveName;
    public GameObject EnemyPrefab; // Enemy type
    public int Count;              // Amount of enemies
    public float SpawnRate;        // Spawn frequency in seconds
    public float StartTime;        // Time when spawning starts
}