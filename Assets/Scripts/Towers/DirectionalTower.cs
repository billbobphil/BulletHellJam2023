using System.Collections;
using Bullets;
using UnityEditor;
using UnityEngine;

namespace Towers
{
    public class DirectionalTower : ShootingTower
    {
        protected override void Shoot()
        {
            base.Shoot();
            GameObject leftBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            leftBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.left;
            
            GameObject rightBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            rightBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.right;
            
            GameObject upBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            upBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.up;
            
            GameObject downBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            downBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.down;
        }
    }
}