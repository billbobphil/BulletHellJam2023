using Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsGamePaused;
        public GridManager gridManager;
        public GameObject playerPrefab;
        public WallGenerator wallGenerator;
        public GameObject enemyPrefab;
        public LevelManager levelManager;

        private void Awake()
        {
            IsGamePaused = false;
        }
        
        private void Start()
        {
            gridManager.GenerateGrid();
            Instantiate(playerPrefab, gridManager.GetCenterOfGrid(), Quaternion.identity);
            wallGenerator.GenerateWalls(-1, gridManager.width, -1, gridManager.height);
            
            Instantiate(enemyPrefab, gridManager.GetBottomLeftOfGrid(), Quaternion.identity);
            Instantiate(enemyPrefab, gridManager.GetTopRightOfGrid(), Quaternion.identity);
            Instantiate(enemyPrefab, gridManager.GetTopLeftOfGrid(), Quaternion.identity);
            Instantiate(enemyPrefab, gridManager.GetBottomRightOfGrid(), Quaternion.identity);
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGamePaused)
                {
                    Time.timeScale = 1;
                    IsGamePaused = false;
                    levelManager.DeactivateBuildPhase();
                }
                else
                {
                    if (levelManager.ActivateBuildPhase())
                    {
                        Time.timeScale = 0;
                        IsGamePaused = true;    
                    }
                }
            }
        }
    }
}