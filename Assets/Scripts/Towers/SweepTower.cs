using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

namespace Towers
{
    public class SweepTower: ShootingTower
    {
        private float angle = 20f;
        private Vector3 direction;
        private int currentDirectionIndex = 0;

        private List<Vector3> directions = new List<Vector3>()
        {
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down,
        };
        
        protected override void Shoot()
        {
            base.Shoot();
            StartCoroutine(FancyShootin());
        }
        
        private IEnumerator FancyShootin()
        {
            //Look, I know it can be a loop with a nice function to take in rotation multiplier, but I'm tired from my day job alright?
            
            direction = directions[currentDirectionIndex];
            
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            const float timeToWait = .3f;
            
            GameObject firstBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            firstBullet.GetComponent<DirectionalTowerBullet>().direction = rotation * direction;
            
            yield return new WaitForSeconds(timeToWait);
            shootAudioSource.Play();
            GameObject secondBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            secondBullet.GetComponent<DirectionalTowerBullet>().direction = rotation * rotation * direction;
            
            yield return new WaitForSeconds(timeToWait);
            shootAudioSource.Play();
            GameObject thirdBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            thirdBullet.GetComponent<DirectionalTowerBullet>().direction = rotation * rotation * rotation * direction;
            
            yield return new WaitForSeconds(timeToWait);
            shootAudioSource.Play();
            GameObject fourthBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            fourthBullet.GetComponent<DirectionalTowerBullet>().direction = rotation * rotation * rotation * rotation * direction;
            
            yield return new WaitForSeconds(timeToWait);
            shootAudioSource.Play();
            GameObject fifthBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            fifthBullet.GetComponent<DirectionalTowerBullet>().direction = rotation * rotation * rotation * rotation * rotation * direction;
            
            if(currentDirectionIndex + 1 >= directions.Count)
            {
                currentDirectionIndex = 0;
            }
            else
            {
                currentDirectionIndex++;
            }
        }
    }
}