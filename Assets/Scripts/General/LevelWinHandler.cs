using System;
using UnityEngine;

namespace General
{
    public class LevelWinHandler : MonoBehaviour
    {
        [SerializeField] private GameObject levelWinPanel;
        
        private void Awake()
        {
            levelWinPanel.SetActive(false);
        }
        
        private void OnEnable()
        {
            WaveManager.OnLastWaveCleared += HandleLevelWin;
        }
        
        private void OnDisable()
        {
            WaveManager.OnLastWaveCleared -= HandleLevelWin;
        }
        
        private void HandleLevelWin()
        {
            GameManager.PauseGame();
            GameManager.BlockBuildInput = true;
            levelWinPanel.SetActive(true);
        }
    }
}