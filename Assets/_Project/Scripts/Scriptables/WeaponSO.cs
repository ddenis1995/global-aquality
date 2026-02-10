using UnityEngine;

namespace _Project.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "New weapon", menuName = "Scriptable Objects/WeaponSO")]
    
    public class WeaponSO : ScriptableObject
    {
        public string Name;
        public float RateOfFire;
        public GameObject ProjectilePrefab;
        public float Damage;
        public float Range;
        public float Speed;
        public TargetingTypes TargetingType;
    }

    public enum TargetingTypes
    {
        Closest,
        Furthest,
        Healthiest,
        MostDamaged,
        Emitting
    }
    
}