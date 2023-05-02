using System;
using Enemies;
using UnityEngine;

namespace General
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource enemyHitAudioSource;
        [SerializeField] private AudioSource buildPhaseToggleAudioSource;

        private void OnEnable()
        {
            EnemyHealthController.AnyEnemyHit += PlayEnemyHitSound;
            LevelManager.OnBuildPhaseToggled += PlayBuildPhaseToggleSound;
        }
        
        private void OnDisable()
        {
            EnemyHealthController.AnyEnemyHit -= PlayEnemyHitSound;
            LevelManager.OnBuildPhaseToggled -= PlayBuildPhaseToggleSound;
        }
        
        private void PlayEnemyHitSound()
        {
            enemyHitAudioSource.Play();
        }
        
        private void PlayBuildPhaseToggleSound()
        {
            buildPhaseToggleAudioSource.Play();
        }
    }
}
