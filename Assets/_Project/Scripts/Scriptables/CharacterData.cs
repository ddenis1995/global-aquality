using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public string NameOrType;
        public float MaxHealth;
        public float DamageBase;
        public float Range;
        public float Cooldown;
        public float Speed;
        public float VisionRange = 30f;
        public WeaponSO[] Weapons;
    }
}
