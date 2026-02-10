using System;
using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Multiplayer? Use ObjectNetwork or ID for player.
    /// </summary>
    
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth Instance { get; private set; }

        private float _health = 100;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void ResetHealth(float maxHealth)
        {
            _health = maxHealth;
        }


        public void TakeDamage(float damage)
        {
            Debug.Log("Player took damage!");
            _health = Mathf.Max(0, _health - damage);
            // I-frames? UI? Death? Here.
            if (_health <= 0) Die();
        }

        void Die()
        {
            Debug.Log("Player died!");
            /* Respawn, etc. */
        }
    }
}