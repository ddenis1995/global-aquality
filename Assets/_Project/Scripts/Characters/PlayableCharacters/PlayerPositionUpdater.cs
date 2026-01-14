using _Project.Scripts.Scriptables;
using UnityEngine;

namespace _Project.Scripts.Characters.PlayableCharacters
{
    public class PlayerPositionSOUpdater : MonoBehaviour
    {
        public PlayerPositionSO PlayerPositionSO;
    
        void Update()
        {
            PlayerPositionSO.Value = this.transform.position;
        }
    }
}
