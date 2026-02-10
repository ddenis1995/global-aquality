using UnityEngine;

namespace _Project.Scripts.Weapons
{
    [System.Serializable]
    public struct Cooldown
    {
        public float Duration;
        private float _readyAt;

        public bool IsReady => Time.time >= _readyAt;
        public float Progress => Mathf.Clamp01((_readyAt - Time.time) / Duration);  // For UI fill (0=ready, 1=full CD)

        public void Trigger() => _readyAt = Time.time + Duration;
        public void Reset() => _readyAt = 0f;
    }
}