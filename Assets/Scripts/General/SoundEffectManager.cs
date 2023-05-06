using System;
using Enemies;
using Player;
using Towers;
using UnityEngine;

namespace General
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource enemyHitAudioSource;
        [SerializeField] private AudioSource buildPhaseToggleAudioSource;
        [SerializeField] private AudioSource playerDeathAudioSource;
        [SerializeField] private AudioSource coinPickupAudioSource;
        [SerializeField] private AudioSource mineTowerExplosionAudioSource;

        private void OnEnable()
        {
            EnemyHealthController.AnyEnemyHit += PlayEnemyHitSound;
            LevelManager.OnBuildPhaseToggled += PlayBuildPhaseToggleSound;
            PlayerHealthController.OnPlayerDeath += PlayPlayerDeathSound;
            CoinManager.CollectedCoin += PlayCoinPickupSound;
            MineTower.MineTriggered += PlayMineExplosionSound;
        }
        
        private void OnDisable()
        {
            EnemyHealthController.AnyEnemyHit -= PlayEnemyHitSound;
            LevelManager.OnBuildPhaseToggled -= PlayBuildPhaseToggleSound;
            PlayerHealthController.OnPlayerDeath -= PlayPlayerDeathSound;
            CoinManager.CollectedCoin -= PlayCoinPickupSound;
            MineTower.MineTriggered -= PlayMineExplosionSound;
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
        
        private void PlayCoinPickupSound(int _)
        {
            coinPickupAudioSource.Play();
        }
        
        private void PlayMineExplosionSound()
        {
            mineTowerExplosionAudioSource.Play();
        }
    }
}
