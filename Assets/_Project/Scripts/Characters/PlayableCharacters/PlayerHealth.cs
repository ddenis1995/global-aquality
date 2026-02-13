using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    /// <summary>
    /// Multiplayer? Use ObjectNetwork or ID for player.
    /// </summary>
    
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth Instance { get; private set; }

        [SerializeField] private Image _barUI;
        [SerializeField] private TMP_Text _healthText;

        private float _health;
        private float _maxHealth = 100;

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
            UpdateUI();
        }


        public void TakeDamage(float damage)
        {
            Debug.Log("Player took damage!");
            _health = Mathf.Max(0, _health - damage);
            // I-frames? UI? Death? Here.
            UpdateUI();
            if (_health <= 0) Die();
        }

        void Die()
        {
            Debug.Log("Player died!");
            /* Respawn, etc. */
        }

        private void UpdateUI()
        {
            //string healthDisplay
            _barUI.fillAmount =  _health /_maxHealth;
            _healthText.text = _health+"/"+_maxHealth;
            Debug.Log(_health / _maxHealth);
        }
    }
}