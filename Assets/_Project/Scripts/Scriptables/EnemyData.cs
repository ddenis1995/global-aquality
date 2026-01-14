using UnityEngine;

namespace _Project.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/EnemyScriptableObject")]
    public class EnemyData : ScriptableObject
    {
        public string EnemyName;
        public int MaxHealth;
        public int Damage;
        public int Speed;
    }
}
