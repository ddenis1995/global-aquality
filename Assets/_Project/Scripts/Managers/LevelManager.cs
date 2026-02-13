using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class LevelManager:MonoBehaviour
    {
        private void Start()
        {
            GameManagement.ResetLevelKills();
        }
    }
}