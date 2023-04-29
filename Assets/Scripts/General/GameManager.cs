using Grid;
using UnityEngine;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        public GridManager gridManager;
        public GameObject playerPrefab;
        public WallGenerator wallGenerator;
        public GameObject enemyPrefab;

        private void Start()
        {
            gridManager.GenerateGrid();
            Instantiate(playerPrefab, gridManager.GetCenterOfGrid(), Quaternion.identity);
            wallGenerator.GenerateWalls(-1, gridManager.width, -1, gridManager.height);
            
            Instantiate(enemyPrefab, gridManager.GetBottomLeftOfGrid(), Quaternion.identity);
            Instantiate(enemyPrefab, gridManager.GetTopRightOfGrid(), Quaternion.identity);
        }
    }
}