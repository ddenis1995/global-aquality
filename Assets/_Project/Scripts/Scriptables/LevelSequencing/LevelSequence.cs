using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Scriptables.LevelSequencing
{
    [CreateAssetMenu(fileName = "LevelSequence", menuName = "Scriptable Objects/LevelSequence")]
    public class LevelSequence : ScriptableObject
    {
        public float LevelDurationInSeconds;
        public WaveData[] Waves;
    }
}
