using UnityEngine;

namespace _Project.Scripts.Scriptables.LevelSequencing
{
    [CreateAssetMenu(fileName = "LevelSequence", menuName = "Scriptable Objects/LevelSequence")]
    public class LevelSequence : ScriptableObject
    {
        public string LevelName = "Level name";
        public WaveData[] Waves;
        
        // public float LevelDurationInSeconds;
        
        [Tooltip("Time to wait after last wave is cleared before level complete / next level")]
        public float TimeAfterLastWave = 5f;

        // Optional level-wide settings
        public float GlobalSpawnVariance = 0.1f;
        
    }
}
