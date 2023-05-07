using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace General
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private SceneAsset nextLevel;
        [SerializeField] private SceneAsset mainMenu;
        private MusicPlayer _musicPlayer;

        private void Awake()
        {
            _musicPlayer = GameObject.FindWithTag("Music")?.GetComponent<MusicPlayer>();
        }
        
        public void ResetLevel()
        {
            ResetStatics();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        [ContextMenu("Load Next Level")]
        public void LoadNextLevel()
        {
            Debug.Log("Loading next level");
            ResetStatics();
            SceneManager.LoadScene(nextLevel.name);
            if (_musicPlayer is not null)
            {
                _musicPlayer.UnPause();    
            }
        }
        
        public void LoadMainMenu()
        {
            ResetStatics();
            SceneManager.LoadScene(mainMenu.name);
            _musicPlayer.Pause();
        }

        private void ResetStatics()
        {
            GameManager.IsGamePaused = false;
            GameManager.IsWaveSpawning = false;
            GameManager.BlockBuildInput = false;
        }
    }
}
