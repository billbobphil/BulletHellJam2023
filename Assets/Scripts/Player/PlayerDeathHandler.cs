using System;
using System.Collections;
using General;
using UnityEngine;

namespace Player
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
        
        private void OnEnable()
        {
            PlayerHealthController.OnPlayerDeath += HandlePlayerDeath;
            LevelManager.TimerExpired += HandlePlayerDeath;
        }
        
        private void OnDisable()
        {
            PlayerHealthController.OnPlayerDeath -= HandlePlayerDeath;
            LevelManager.TimerExpired -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            _gameManager.gameOverPanel.SetActive(true);
            StartCoroutine(PauseAfterDeath());
        }

        private IEnumerator PauseAfterDeath()
        {
            yield return new WaitForSecondsRealtime(.5f);
            GameManager.PauseGame();
            GameManager.BlockBuildInput = true;
        }
    }
}