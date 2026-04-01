using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; // if using your own loading screen

namespace _Project.Scripts.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [Header("Settings")] [SerializeField] private GameObject _loadingScreen; // Canvas with loading
        [SerializeField] private Slider _progressBar; // Optional
        [SerializeField] private Text _progressText; // Optional

        private string currentLevelName = "";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Key moment!
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Loading level from hub
        /// </summary>
        public void LoadLevel(string levelName)
        {
            currentLevelName = levelName;
            StartCoroutine(LoadLevelCoroutine(levelName));
        }

        /// <summary>
        /// Returning to hub from level
        /// </summary>
        public void ReturnToHub()
        {
            StartCoroutine(LoadLevelCoroutine("HubScene")); // Hub scene name
        }

        private IEnumerator LoadLevelCoroutine(string sceneName)
        {
            // Showing loading screen
            if (_loadingScreen != null)
                _loadingScreen.SetActive(true);

            // Saving everything important before unloading the current scene
            SaveSystem.SaveProgress(); // ← your save class

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            operation.allowSceneActivation = false; // Wait until we're ready

            float progress = 0f;

            while (!operation.isDone)
            {
                progress = Mathf.Clamp01(operation.progress / 0.9f);

                // Refresh loading UI
                if (_progressBar != null) _progressBar.value = progress;
                if (_progressText != null) _progressText.text = $"{(progress * 100):F0}%";

                // when load is almost done - activate scene(questionable)
                if (operation.progress >= 0.9f)
                {
                    // If needed, add small pause for the beauty of it
                    yield return new WaitForSeconds(0.3f);
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }

            // After unloaded fully
            if (sceneName == "HubScene")
            {
                // Loaded the hub - load data
                SaveSystem.LoadProgress();
                currentLevelName = "";
            }

            if (_loadingScreen != null)
                _loadingScreen.SetActive(false);
        }

        // Convenient method of calling from levels
        public static void GoToHub()
        {
            if (Instance != null)
                Instance.ReturnToHub();
        }
    }
}