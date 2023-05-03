using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace General
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private SceneAsset nextLevel;
        [SerializeField] private SceneAsset mainMenu;

        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(nextLevel.name);
        }
        
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(mainMenu.name);
        }
    }
}
