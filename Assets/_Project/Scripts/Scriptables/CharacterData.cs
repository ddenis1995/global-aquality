using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public string Name;
        public int MaxHealth;
        public int DamageBase;
        public int Speed;
        public WeaponSO[] Weapons;
    }
}
