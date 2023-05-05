using System;
using System.Collections;
using UnityEngine;
using Utilities;

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
            StartCoroutine(ShowGameWinUi());
        }

        private IEnumerator ShowGameWinUi()
        {
            yield return new WaitForSecondsRealtime(.2f);
            GameManager.PauseGame();
            GameManager.BlockBuildInput = true;
            levelWinPanel.SetActive(true);
        }
    }
}