using System.Collections;
using Bullets;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyShootingController : MonoBehaviour
    {
        private Transform _playerTransform;
        public GameObject bulletPrefab;
        public float fireRateSeconds;
        [SerializeField] private AudioSource shootAudioSource;
        
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
            _playerTransform = GameObject.FindWithTag("Player").transform;
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            for (;;)
            {
                yield return new WaitForSeconds(fireRateSeconds);
                Shoot();
            }
        }

        private void Shoot()
        {
            shootAudioSource.Play();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            bullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(direction.x, direction.y, 0);
        }
    }
}