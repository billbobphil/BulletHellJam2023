using System.Collections;
using Bullets;
using UnityEngine;

namespace Enemies
{
    public class EnemyShootingController : MonoBehaviour
    {
        private Transform _playerTransform;
        public GameObject bulletPrefab;
        public float fireRateSeconds;

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
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            bullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(direction.x, direction.y, 0);
        }
    }
}