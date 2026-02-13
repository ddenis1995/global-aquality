using System.Linq;
using System.Text;
using _Project.Scripts.Managers;
using UnityEngine;
using TMPro;

namespace _Project.Scripts.UI
{
    public class KillCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killText;

        private void OnEnable()
        {
            GameManagement.OnKillRegistered += UpdateUI;
        }

        private void OnDisable()
        {
            GameManagement.OnKillRegistered -= UpdateUI;
        }

        private void Start()
        {
            if (_killText == null)
            {
                Debug.LogWarning("KillText reference is missing!", this);
                return;
            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_killText == null) return;

            var sb = new StringBuilder();
            sb.AppendLine($"Total Kills: {GameManagement.KillCount}");

            if (GameManagement.TypeKillCounts.Count > 0)
            {
                // Sort by kill count descending for nice display
                foreach (var kvp in GameManagement.TypeKillCounts.OrderByDescending(x => x.Value))
                {
                    sb.Append(kvp.Key);
                    sb.Append(": ");
                    sb.Append(kvp.Value);
                    sb.AppendLine();
                }
            }

            _killText.text = sb.ToString();
        }

        // Public call for manual refresh (e.g., after level load)
        public void Refresh()
        {
            UpdateUI();
        }
    }
}