using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace General
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private int nextLevel;
        [SerializeField] private int mainMenu;
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
            SceneManager.LoadScene(nextLevel);
            if (_musicPlayer is not null)
            {
                _musicPlayer.UnPause();    
            }
        }
        
        public void LoadMainMenu()
        {
            ResetStatics();
            SceneManager.LoadScene(mainMenu);
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
