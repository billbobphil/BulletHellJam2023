using System.Collections;
using Bullets;
using Enemies.Guns;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyShootingController : MonoBehaviour
    {
        public float fireRateSeconds;
        [SerializeField] private EnemyGun enemyGun;
        
        private void OnEnable()
        {
            PlayerHealthController.OnPlayerDeath += HandlePlayerDeath;
        }
        
        private void OnDisable()
        {
            PlayerHealthController.OnPlayerDeath -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            StopAllCoroutines();
        }

        private void Start()
        {
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            for (;;)
            {
                yield return new WaitForSeconds(fireRateSeconds);
                // Shoot();
                enemyGun.Shoot();
            }
        }
    }
}