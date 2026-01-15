using UnityEngine;

namespace _Project.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
    
    public class WeaponSO : ScriptableObject
    {
        public int Range;
        public int Damage;
        public int RateOfFire;
        public GameObject ProjectilePrefab;

        public void Fire()
        {
        }
    }
}