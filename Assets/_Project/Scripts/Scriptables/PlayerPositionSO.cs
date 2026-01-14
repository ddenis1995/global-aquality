using UnityEngine;

namespace _Project.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "PlayerPositionSO", menuName = "Scriptable Objects/PlayerPositionSO")]
    public class PlayerPositionSO : ScriptableObject
    {
        public Vector3 Value;
    }
}
