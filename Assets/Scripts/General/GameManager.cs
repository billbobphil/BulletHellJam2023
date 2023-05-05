using Grid;
using UnityEngine;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsGamePaused;
        public static bool IsWaveSpawning;
        public static bool BlockBuildInput;
        public GridManager gridManager;
        public GameObject playerPrefab;
        public WallGenerator wallGenerator;
        public LevelManager levelManager;
        [SerializeField] private GameObject backgroundImage;
        public GameObject gameOverPanel;

        private void Awake()
        {
            IsGamePaused = false;
            IsWaveSpawning = false;
            BlockBuildInput = false;
            gameOverPanel.SetActive(false);
        }
        
        private void Start()
        {
            gridManager.GenerateGrid();
            Instantiate(playerPrefab, gridManager.GetCenterOfGrid(), Quaternion.identity);
            wallGenerator.GenerateWalls(-1, gridManager.width, -1, gridManager.height);
            backgroundImage.transform.position = gridManager.GetCenterOfGrid();
        }

        public static void PauseGame()
        {
            IsGamePaused = true;
            Time.timeScale = 0;
        }

        public static void ResumeGame()
        {
            IsGamePaused = false;
            Time.timeScale = 1;
        }

        private void LateUpdate()
        {
            if (BlockBuildInput) return;
            
            if (!IsWaveSpawning && Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGamePaused)
                {
                    // Time.timeScale = 1;
                    // IsGamePaused = false;
                    ResumeGame();
                    levelManager.DeactivateBuildPhase();
                }
                else
                {
                    if (levelManager.ActivateBuildPhase())
                    {
                        // Time.timeScale = 0;
                        // IsGamePaused = true;    
                        PauseGame();
                    }
                }
            }
        }
    }
}