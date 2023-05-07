using System;
using System.Collections;
using Bullets;
using Enemies;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Towers
{
    public class MineTower : Tower
    {
        [SerializeField] private float secondsToArm;
        public bool isArmed;
        public int mineExplosionDamage;
        [SerializeField] private GameObject bulletPrefab;
        public static UnityAction MineTriggered;

        private void Start()
        {
            StartCoroutine(Arm());
        }

        private IEnumerator Arm()
        {
            yield return new WaitForSeconds(secondsToArm);
            isArmed = true;
            Animator.SetBool("IsArmed", true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("BossHand"))
            {
                TriggerMine(other.gameObject);    
            }
        }
        
        public void TriggerMine(GameObject objectThatSteppedOnMine)
        {
            if (!isArmed) return;
            
            if (objectThatSteppedOnMine.CompareTag("Enemy"))
            {
                MineTriggered?.Invoke();
                objectThatSteppedOnMine.GetComponent<EnemyHealthController>().HitEnemy(mineExplosionDamage);
                FireShrapnel();
            }
            else if (objectThatSteppedOnMine.CompareTag("BossHand"))
            {
                MineTriggered?.Invoke();
                objectThatSteppedOnMine.GetComponent<BossHand>().bossHealthController.HitEnemy(2);
                FireShrapnel();
            }
            else if (objectThatSteppedOnMine.CompareTag("Player"))
            {
                MineTriggered?.Invoke();
                FireShrapnel();
            }
        }

        private void FireShrapnel()
        {
            GameObject leftBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            leftBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.left;
            
            GameObject topRightBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            topRightBullet.GetComponent<DirectionalTowerBullet>().direction = new Vector3(1, 1, 0);
            
            GameObject rightBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            rightBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.right;
            
            GameObject topLeftBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            topLeftBullet.GetComponent<DirectionalTowerBullet>().direction = new Vector3(-1, 1, 0);
            
            GameObject upBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            upBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.up;
            
            GameObject bottomRightBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bottomRightBullet.GetComponent<DirectionalTowerBullet>().direction = new Vector3(1, -1, 0);
            
            GameObject downBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            downBullet.GetComponent<DirectionalTowerBullet>().direction = Vector3.down;
            
            GameObject bottomLeftBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bottomLeftBullet.GetComponent<DirectionalTowerBullet>().direction = new Vector3(-1, -1, 0);
            
            Destroy(gameObject);
        }
    }
}