using Bullets;
using UnityEngine;

namespace Enemies.Guns
{
    public class SpreadShotTowardsPlayerGun : EnemyGun
    {
        private Transform _playerTransform;
        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
        
        public override void Shoot()
        {
            base.Shoot();
            GameObject bulletMiddle = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            bulletMiddle.GetComponent<DirectionalEnemyBullet>().direction = new Vector3(direction.x, direction.y, 0);

            const float angle = 20.0f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject bulletLeft = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletLeft.GetComponent<DirectionalEnemyBullet>().direction = rotation * direction;

            Quaternion reverseRotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            
            GameObject bulletRight = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletRight.GetComponent<DirectionalEnemyBullet>().direction = reverseRotation * direction;
        }
    }
}