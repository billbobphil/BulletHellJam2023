using System;
using Enemies;
using Player;
using UnityEngine;

namespace General
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource enemyHitAudioSource;
        [SerializeField] private AudioSource buildPhaseToggleAudioSource;
        [SerializeField] private AudioSource playerDeathAudioSource;

        private void OnEnable()
        {
            EnemyHealthController.AnyEnemyHit += PlayEnemyHitSound;
            LevelManager.OnBuildPhaseToggled += PlayBuildPhaseToggleSound;
            PlayerHealthController.OnPlayerDeath += PlayPlayerDeathSound;
        }
        
        private void OnDisable()
        {
            EnemyHealthController.AnyEnemyHit -= PlayEnemyHitSound;
            LevelManager.OnBuildPhaseToggled -= PlayBuildPhaseToggleSound;
            PlayerHealthController.OnPlayerDeath -= PlayPlayerDeathSound;
        }
        
        private void PlayEnemyHitSound()
        {
            enemyHitAudioSource.Play();
        }
        
        private void PlayBuildPhaseToggleSound()
        {
            buildPhaseToggleAudioSource.Play();
        }
        
        private void PlayPlayerDeathSound()
        {
            playerDeathAudioSource.Play();
        }
    }
}
