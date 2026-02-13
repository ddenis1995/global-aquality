using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class GameManagement:MonoBehaviour
    {
        public static GameManagement Instance { get; private set; }
    
        public static int KillCount { get; private set; } = 0;
        
        private static readonly Dictionary<string, int> _typeKillCounts = new Dictionary<string, int>();
        public static IReadOnlyDictionary<string, int> TypeKillCounts => _typeKillCounts;

        public static event Action OnKillRegistered;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void RegisterKill(string enemyType)
        {
            if (string.IsNullOrEmpty(enemyType))
            {
                Debug.LogWarning("RegisterKill called with empty enemyType!");
                return;
            }

            if (_typeKillCounts.TryGetValue(enemyType, out int current))
            {
                _typeKillCounts[enemyType] = current + 1;
            }
            else
            {
                _typeKillCounts[enemyType] = 1;
            }

            KillCount++;
            OnKillRegistered?.Invoke();
            //Debug.Log($"Kill registered: {enemyType} (Total: {KillCount})");
        }

        // Reset for new level (clears types and total)
        public static void ResetLevelKills()
        {
            KillCount = 0;
            _typeKillCounts.Clear();
            OnKillRegistered?.Invoke();  // Refresh UI
            Debug.Log("Level kill counters reset.");
        }
    }
    
}