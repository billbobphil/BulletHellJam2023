using Bullets;
using UnityEngine;

namespace Enemies.Guns
{
    public class SingleShotTowardsPlayerGun : EnemyGun
    {
        private Transform _playerTransform;
        
        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
        
        public override void Shoot()
        {
            base.Shoot();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            bullet.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(direction.x, direction.y, 0);
        }
    }
}